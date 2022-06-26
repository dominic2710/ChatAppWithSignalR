using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.Services.Authenticate
{
    public class AuthenticateRequest
    {
        public string LoginId { get; set; }
        public string Password { get; set; }
    }
}
