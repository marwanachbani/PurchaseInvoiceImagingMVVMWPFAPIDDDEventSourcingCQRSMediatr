using Core;
using Data.SQLServer;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCS.Commands;
using UserCS.Services;

namespace UserCS.Handlers
{
    public class ConnectHandler : IRequestHandler<Connect, BaseResponse>
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly SignInManager<UserApp> _signInManager;
        private readonly ITokenServices _tokenServices;

        public ConnectHandler(SignInManager<UserApp> signInManager, UserManager<UserApp> userManager, ITokenServices tokenServices)
        {

            _signInManager = signInManager;
            _userManager = userManager;
            _tokenServices = tokenServices;
        }

        public async Task<BaseResponse> Handle(Connect request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var response = new BaseResponse();
            if(user== null)
            {
                response.Successed= false;
                response.Response = request.UserName +" is wrong";
                await Task.CompletedTask; return response;
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.PassWord, true);
            if (!result.Succeeded)
            {
                response.Successed = false;
                response.Response = "the password is wrong";
                await Task.CompletedTask;
                return response;
            }
            response.Successed = true; 
            response.Response = _tokenServices.GetToken(user);
            await Task.CompletedTask;
            return response;
            
        }
    }
}
