# GCloud-VM-REST
A REST interface to create a Google Cloud VM. Implemented using ASP.NET Web API using Google Cloud .NET SDK

To run this API, you'll need Visual Studio (preferably 2015 or later) installed. IIS Express bundled with it should be enough to run locally.

Building the solution should automatically do a Nuget restore and install the GCloud .NET SDKs required for the VM creation.

You need to create a service account from GCloud console or use an existing one with owner permissions enabled on GCloud Compute resource.

Create a service account key from API Manager -> Credentials from the Google Cloud console with the above service account associated with it.
Select the JSON option to download the certificate details as JSON. Place it in the root directory of the project. I have a sample in my solution.
You can replace the values with your credentials. 

The web.config has a app setting key ("JSONFileName") which is the path to the JSON file which was downloaded above. Either use the existing file
or update the file name with the JSON file you downloaded.

To create a VM, do a HTTP POST request to 

```
localhost:<port>/api/virtualmachines/create
```
with following JSON (sample example)

```
{
	"Name":"testdemo1",
	"CPUType":"zones/us-central1-f/machineTypes/n1-standard-1",
	"ProjectId":"gcloudapi",
	"Zone":"us-central1-f",
	"SourceDiskImage":"projects/debian-cloud/global/images/family/debian-8",
	"NetworkInterface":"global/networks/default"
}
```

The response returns the ID and status of the virtual machine that was created if it was a success. Otherwise a 401 or 500 is returned
depending on the request.

Currently, the API only supports one disk and network interface attachment. This can be modified fairly easily if required.