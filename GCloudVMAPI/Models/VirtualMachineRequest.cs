using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCloudVMAPI.Models
{
    /// <summary>
    /// Initial request to create VM
    /// </summary>
    public class VirtualMachineRequest
    {
        /// <summary>
        /// gets the virtual machine name
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        //private variable storing the name
        private string _name;

        /// <summary>
        /// GCloud Project Id
        /// </summary>
        public string ProjectId
        {
            get
            {
                return _projectId;
            }
            set
            {
                _projectId = value;
            }
        }

        private string _projectId;

        /// <summary>
        /// Zone/region in which VM is to be created
        /// </summary>
        public string Zone
        {
            get
            {
                return _zone;
            }
            set
            {
                _zone = value;
            }
        }

        private string _zone;

        /// <summary>
        /// Machine type -  the partial URL for the zone specific VM to be created
        /// </summary>
        public string CPUType
        {
            get
            {
                return _cputype;
            }
            set
            {
                _cputype = value;
            }
        }

        private string _cputype;

        /// <summary>
        /// The boot image that needs to be created in a newly attached disk
        /// </summary>
       public string SourceDiskImage
        {
            get
            {
                return _sourcediskimage;
            }
            set
            {
                _sourcediskimage = value;
            }
        }

        private string _sourcediskimage;

        /// <summary>
        /// The partial url for the zone specific network interface
        /// </summary>
        public string NetworkInterface
        {
            get
            {
                return _networkInterface;
            }
            set
            {
                _networkInterface = value;
            }
        }

        private string _networkInterface;
    }
}