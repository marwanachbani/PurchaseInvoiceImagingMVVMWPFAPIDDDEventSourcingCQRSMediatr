using Core;
using Core.Events;
using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCS.Commands;

namespace UserCS
{
    public class UserAggregate : BaseAggregate
    {
        private string _email;
        private string _userName;
        private string _fullName;
        private string _password;
        private string _rePassword;
        private string _activatedBy;

        public string Email { get => _email; set => _email = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public string FullName { get => _fullName; set => _fullName = value; }
        public string Password { get => _password; set => _password = value; }
        public string RePassword { get => _rePassword; set => _rePassword = value; }
        public string ActivatedBy { get => _activatedBy; set => _activatedBy = value; }
        public UserAggregate(IMediator mediator) : base(mediator)
        {
        }
        //-------------------------------------------Logging-------------------------------------------------------
        public async Task<BaseResponse>Connect(string username, string password)
        {
            await SetConnectParamaters(username, password);
            var response = await SendConnectUserCommand(_userName, _password); 
            if (response.Successed == false)
            {
                await PublishUserConnetedFailed(_userName,_password,response.Response);
                return response; 
            }
            await PublishUserConnected(_userName, response.Response); 
            return response;
            
        }
        
        private async Task SetConnectParamaters(string userName , string password)
        {
            _userName = userName;
            _password = password;
            await Task.CompletedTask;
        }
        private async Task PublishUserConnected(string userName , string respnse )
        {
            this.PublishEvent(new UserLogedIn { UserName = userName ,Aggregate = "user" , CommittedBy = userName ,Response = respnse });
            await Task.CompletedTask; 
        }
        private async Task PublishUserConnetedFailed(string userName , string password , string response)
        {
            this.PublishEvent(new UserLoggedinFailed { UserName = userName, Aggregate = "user", CommittedBy = userName,Password = password ,  Response = response });
            await Task.CompletedTask; 
        }
        private async Task<BaseResponse>SendConnectUserCommand(string username, string password)
        {
            var response = await this.SendCommand(new Connect { UserName = username, PassWord = password });
            return response; 
        }
        //--------------------------------------------------Register----------------------------------------------
        public async Task<BaseResponse>Register(string email,string fullName , string userName , string password ,string rePassword )
        {
            await SetRegisterParameters(email, fullName, userName, password, rePassword);
            var response = await SendRegisterUserCommand(_email, _fullName, _userName, _password, _rePassword);
            if(response.Successed==false) {
               await PublishUserRegisteredFailed(_email, _fullName , _userName , _password ,_rePassword, response.Response);
                return response;
            }
            await PublishUserRegistered(_email, _fullName, _userName, _password, _rePassword, response.Response);
            return response; 
        }
        private async Task SetRegisterParameters(string email, string fullName, string userName, string password, string rePassword )
        {
            _userName = userName;
            _password = password;
            _fullName = fullName;
            _email = email;
            _rePassword = rePassword;

            await Task.CompletedTask;
        }
        private async Task PublishUserRegistered(string email, string fullName, string userName, string password, string rePassword, string response)
        {
            this.PublishEvent(new UserRegistered
            {
                Aggregate = "user ",
                CommittedBy = userName,
                Response = response,
                Email = email,
                FullName = fullName,
                UserName = userName,
            }); ;
            await Task.CompletedTask;
        }
        private async Task PublishUserRegisteredFailed(string email, string fullName, string userName, string password, string rePassword, string response)
        {
            this.PublishEvent(new RegisterUserFailed
            {
                Aggregate = "user ",
                CommittedBy = userName,
                Response = response,
                Email = email,
                FullName = fullName,
                Name = "user registered",
                UserName = userName,
            }); ;
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendRegisterUserCommand(string email, string fullName, string userName, string password, string rePassword)
        {
            var response = await this.SendCommand(new Register
            {
                UserName = userName,
                Email = email,
                FullName = fullName,
                Password = password , 
                RePassword = rePassword

            }) ;
            return response;
        }
        //--------------------------------------------------ActivateUser-----------------------------------------------
        public async Task<BaseResponse>ActivateUser(string userName , string activatedBy)
        {
            await SetUserActivationParameters(userName, activatedBy); 
            var response = await SendActivateUserCommand(_userName, _activatedBy);
            if(response.Successed == false)
            {
                await PublishUserActivationFailed(_userName , _activatedBy , response.Response);
                return response;
            }
            await PublishUserActivated(_userName, _activatedBy, response.Response);
            return response;
        }
        private async Task SetUserActivationParameters(string userName , string activateBy)
        {
            _userName = userName; 
            _activatedBy = activateBy;
            await Task.CompletedTask;
        }
        private  async Task PublishUserActivated(string username , string committedBy , string response)
        {
            this.PublishEvent(new UserActivated
            {
                UserName = username , CommittedBy = committedBy , Response = response
            }) ;
            await Task.CompletedTask;
        }
        private async Task PublishUserActivationFailed(string username, string committedBy, string response)
        {
            this.PublishEvent(new UserActivationFailed
            {
                UserName = username,
                CommittedBy = committedBy,
                Response = response
            });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse>SendActivateUserCommand(string userName, string activateBy)
        {
            var response = await this.SendCommand(new ActivateUser { UserName = userName , ActivatBy = activateBy });
            return response; 
        }

    }
}
