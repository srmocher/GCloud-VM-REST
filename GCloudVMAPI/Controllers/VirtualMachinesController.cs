using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Google.Apis.Compute.v1;
using GCloudVMAPI.Models;
using Google.Apis.Services;
using System.Configuration;
using Google.Apis.Compute.v1.Data;
using static Google.Apis.Compute.v1.AddressesResource;
using System.IO;
using System.Text;
using Google.Apis.Auth.OAuth2;
using System.Threading.Tasks;

namespace GCloudVMAPI.Controllers
{

    public class VirtualMachinesController : ApiController
    {
        [HttpPost]
        public async Task<IHttpActionResult> Create(VirtualMachineRequest request)
        {
            //Invalid request
            if (request == null || request.CPUType == null || request.Name == null || request.Zone == null || request.ProjectId == null || request.SourceDiskImage == null || request.NetworkInterface == null)
                return BadRequest("Request missing one or more parameters");
            //create credentials JSON file path
            string path = System.Web.HttpContext.Current.Request.MapPath(ConfigurationManager.AppSettings["JSONFileName"]);
          
            //create credentials from JSON with scope for Compute read/write
            ICredential cred = GoogleCredential.FromJson(File.ReadAllText(path)).CreateScoped(new string[] { ComputeService.Scope.Compute });
            ComputeService computeService = null;
            try
            {
                //create compute service instance
                using (computeService = new ComputeService())
                {

                    cred.Initialize(computeService.HttpClient);

                    //acquire token
                    string token = await cred.GetAccessTokenForRequestAsync();

                    //add Bearer token in header
                    computeService.HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                   
                    //create instance object
                    Instance instance = new Instance();

                    //set the parameters
                    instance.MachineType = request.CPUType;
                    instance.Name = request.Name;

                    //supports only interface currently
                    NetworkInterface intf = new NetworkInterface();
                    intf.Network = request.NetworkInterface;

                    //one disk attachment - boot disk
                    AttachedDisk disk = new AttachedDisk();
                    disk.InitializeParams = new AttachedDiskInitializeParams();
                    disk.InitializeParams.SourceImage = request.SourceDiskImage;
                    disk.Boot = true;


                    instance.NetworkInterfaces = new List<NetworkInterface> { intf };
                    instance.Disks = new List<AttachedDisk> { disk };

                    //create the request to be serviced
                    InstancesResource.InsertRequest createRequest = computeService.Instances.Insert(instance, request.ProjectId, request.Zone);
                    
                    //execute the request, create VM
                    Operation result = createRequest.Execute();

                    //on success, no exception raised, return the response
                    VirtualMachineResponse response = new VirtualMachineResponse();
                    response.Id = Convert.ToString(result.Id);
                    response.Status = result.Status;
                    response.StatusMessage = result.StatusMessage;

                    return Ok(response);
                }
            }
            catch(Exception e)
            {
                //return 500 Internal Server error with message
                return InternalServerError(e);
            }
            finally
            {
                if (computeService != null)
                    computeService.Dispose();
            }          
        }
    }
}
