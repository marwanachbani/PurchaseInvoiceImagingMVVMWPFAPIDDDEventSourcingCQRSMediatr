using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCS.Bridge
{
    public class UserCommandBridge : IUserCommandBridge
    {
        private readonly IMediator _mediator;

        public UserCommandBridge(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BaseResponse> Activate(string username, string activateBy)
        {
            var aggregate = new UserAggregate(_mediator); 
            var response = await aggregate.ActivateUser(username, activateBy);
            return response; 
        }

        public async Task<BaseResponse> Login(string username, string password)
        {
            var aggregate = new UserAggregate(_mediator); 
            var response = await aggregate.Connect(username, password);
            return response; 
        }

        public async Task<BaseResponse> Register(string username, string password, string rePassword, string email, string fullName)
        {
            var aggregate = new UserAggregate(_mediator);
            var response = await aggregate.Register(email, fullName, username, password, rePassword);
            return response;
        }
    }
    public interface IUserCommandBridge
    {
        Task<BaseResponse>Login(string username, string password);
        Task<BaseResponse>Register(string username, string password , string rePassword , string email  , string fullName);
        Task<BaseResponse>Activate(string username, string activateBy);
    }
}
