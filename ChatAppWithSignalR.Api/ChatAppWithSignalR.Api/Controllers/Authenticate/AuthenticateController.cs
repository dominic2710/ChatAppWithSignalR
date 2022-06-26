using ChatAppWithSignalR.Api.Functions.User;
using Microsoft.AspNetCore.Mvc;

namespace ChatAppWithSignalR.Api.Controllers.Authenticate
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticateController : Controller
    {
        private IUserFunction _userFunction;

        public AuthenticateController(IUserFunction userFunction)
        {
            _userFunction = userFunction;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest request)
        {
            var response = _userFunction.Authenticate(request.LoginId, request.Password);
            if (response == null)
                return BadRequest(new { StatusMessage = "Invalid username or password!" });

            return Ok(response);
        }
    }
}
