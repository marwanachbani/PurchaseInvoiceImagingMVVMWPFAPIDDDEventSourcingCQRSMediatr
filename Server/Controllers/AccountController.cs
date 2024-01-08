using Azure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Commands;
using Server.Response;
using UserCS.Bridge;

namespace Server.Controllers
{
    
    public class AccountController : BaseController
    {
        private readonly IUserCommandBridge _commandBridge;

        public AccountController(IUserCommandBridge commandBridge)
        {
            _commandBridge = commandBridge;
        }

        [HttpPost("activateUser")]
        [Authorize]
        public async Task <CommandResponse>Activate(string userName , string activateBy)
        {
            var response = await _commandBridge.Activate(userName, activateBy);
            return new CommandResponse(response.Successed, response.Response);
        }
        [HttpPost("login")]
        public async Task<CommandResponse> Login([FromBody]Login login)
        {
            var response = await _commandBridge.Login(login.UserName,login.Password);
            return new CommandResponse(response.Successed, response.Response);
        }
        [HttpPost("register")]
        public async Task<CommandResponse>Register(string userName ,string email , string fullName , string password ,string repassword)
        {
            var response = await _commandBridge.Register(userName, password, repassword, email, fullName);
            return new CommandResponse(response.Successed, response.Response);
        }
        
    }
}
