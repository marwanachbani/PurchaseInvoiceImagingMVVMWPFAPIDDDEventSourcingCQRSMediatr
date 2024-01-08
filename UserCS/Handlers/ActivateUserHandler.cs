using Core;
using Data.SQLServer;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCS.Commands;

namespace UserCS.Handlers
{
    public class ActivateUserHandler : IRequestHandler<ActivateUser, BaseResponse>
    {
        private readonly UserManager<UserApp> _userManager;

        public ActivateUserHandler(UserManager<UserApp> userManager)
        {
            _userManager = userManager;
        }

        public async Task<BaseResponse> Handle(ActivateUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            if(user == null)
            {
                return new BaseResponse { Successed = false, Response = "the account doesn't exist" }; 
            }
            user.IsActivated = true;
            await Task.CompletedTask; 
            await _userManager.UpdateAsync(user);
            return new BaseResponse { Successed = true, Response = request.UserName + "is activated successfully" }; 
        }
    }
}
