using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UIApi;
using UIApi.Responses;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public partial class LoginViewModel : ObservableRecipient
    {
        private readonly ApiHelper apiHelper = new ApiHelper();
        public LoginViewModel()
        {
           
        }
        
        [ObservableProperty]
        private string _userName;
        [ObservableProperty]
        private string _password;
        [ObservableProperty]
        private string _response;
        [RelayCommand]
        public async Task Login()
        {
            var response = await apiHelper.Login(UserName, Password);
            if(response.Successed == true) 
            {
                var list = await apiHelper.GelAllForPeperPusher(response.Respnse);
                var res = new UserResponse(UserName, response.Respnse , new AppZone() , list);
                var message = new UserLogedIn(res);
                Messenger.Send(message);
            }
            Response = response.Respnse;
            await Task.CompletedTask;
            
        }
    }
    public sealed class UserLogedIn : ValueChangedMessage<UserResponse>
    {
        public UserLogedIn(UserResponse value) : base(value)
        {

        }

    }
    public class UserResponse
    {
        public UserResponse(string userName, string response, UserControl control, ObservableCollection<All> alls)
        {
            UserNameMessage = userName;
            ResponseMessage = response;
            Control = control;
            Alls = alls;
        }
        public string UserNameMessage { get; set;}
        public string ResponseMessage { get; set; }
        public UserControl Control { get; set; }
        public ObservableCollection<All> Alls { get; set; }
    }
}
