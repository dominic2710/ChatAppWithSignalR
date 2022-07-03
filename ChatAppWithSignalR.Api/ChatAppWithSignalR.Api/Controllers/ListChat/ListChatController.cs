namespace ChatAppWithSignalR.Api.Controllers.ListChat
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ListChatController : Controller
    {
        IUserFunction _userFunction;
        IUserFriendFunction _userFriendFunction;
        IMessageFunction _messageFunction;

        public ListChatController(IUserFunction userFunction, IUserFriendFunction userFriendFunction, IMessageFunction messageFunction)
        {
            _userFunction = userFunction;
            _userFriendFunction = userFriendFunction;
            _messageFunction = messageFunction;
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody] int userId)
        {
            var response = new ListChatInitializeResponse
            {
                User = _userFunction.GetUserById(userId),
                UserFriends = await _userFriendFunction.GetListUserFriend(userId),
                LastestMessages = await _messageFunction.GetLastestMessage(userId)
            };

            return Ok(response);
        }
    }
}
