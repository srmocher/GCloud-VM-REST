using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCloudVMAPI.Models
{
    /// <summary>
    /// Success Response on virtual machine creation request
    /// </summary>
    public class VirtualMachineResponse
    {
        private string _status;
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        private string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private string _statusmessage;
        public string StatusMessage
        {
            get
            {
                return _statusmessage;
            }
            set
            {
                _statusmessage = value;
            }
        }
    }
}