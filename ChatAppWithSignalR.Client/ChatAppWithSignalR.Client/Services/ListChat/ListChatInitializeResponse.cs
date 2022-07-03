using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppWithSignalR.Client.Services.ListChat
{
    public class ListChatInitializeResponse : BaseResponse
    {
        public User User { get; set; }
        public IEnumerable<User> UserFriends { get; set; }
        public IEnumerable<LastestMessage> LastestMessages { get; set; }
    }
}
