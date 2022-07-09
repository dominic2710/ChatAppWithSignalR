using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.Services.Message
{
    public class MessageInitializeReponse:BaseResponse
    {
        public User FriendInfo { get; set; }
        public IEnumerable <Models.Message> Messages { get; set; }
    }
}
