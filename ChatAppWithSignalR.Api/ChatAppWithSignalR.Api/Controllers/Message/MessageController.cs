using Microsoft.AspNetCore.Mvc;

namespace ChatAppWithSignalR.Api.Controllers.Message
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class MessageController : Controller
    {
        IMessageFunction _messageFunction;
        IUserFunction _userFunction;

        public MessageController(IMessageFunction messageFunction, IUserFunction userFunction)
        {
            _messageFunction = messageFunction;
            _userFunction = userFunction;
        }

        [HttpPost("Initialize")]
        public async Task<ActionResult> Initialize([FromBody  ] MessageInitalizeRequest request)
        {
            var response = new MessageInitalizeResponse
            {
                FriendInfo = _userFunction.GetUserById(request.ToUserId),
                Messages =await _messageFunction.GetMessages(request.FromUserId, request.ToUserId)
            };

            return Ok(response);
        }
    }
}
