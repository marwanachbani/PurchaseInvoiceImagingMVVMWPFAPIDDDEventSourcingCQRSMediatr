using Core;
using Data.SQLServer;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCS.Commands;

namespace UserCS.Handlers
{
    public class RegisterHandler : IRequestHandler<Register, BaseResponse>
    {
        private readonly IdentityAppContext _identity; 
        private readonly UserManager<UserApp> _userManager;

        public RegisterHandler(IdentityAppContext identity, UserManager<UserApp> userManager)
        {
            _identity = identity;
            _userManager = userManager;
        }

        public async Task<BaseResponse> Handle(Register request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user!=null)
            {
                return new BaseResponse { Successed = false , Response = "the email already used" };
            }

            var userModel = new UserApp
            {
                
                Email = request.Email,
                UserName = request.UserName
            };

            var result = await _userManager.CreateAsync(userModel, request.Password);

            if (!result.Succeeded) return new BaseResponse { Successed = false, Response = " try another password"};
            return new BaseResponse { Successed = true, Response = "your account is " +
                "created successefuly please keep in touch untill is activated" }; 

        }
    }
}
