using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIApi;


namespace WpfApp1.ViewModels
{
    public partial class CreateNewContentVM : ObservableRecipient
    {
       
        
        private readonly ApiHelper apiHelper = new ApiHelper();
        public CreateNewContentVM()
        {
            Content = DateTime.Now;
            Messenger.Register<CreateNewContentVM, OnOpenCreateContent>(this, (r, m) =>

            GetDataFromLogin(m.Value.User, m.Value.Token));
        }
        private string username;
        public string Username
        {
            get => username;
            private set => SetProperty(ref username, value);
        }
        private string token;

        public string Token
        {
            get => token;
            private set => SetProperty(ref token, value);
        }
        private string response;
        public string Response
        {
            get => response;
            private set => SetProperty(ref response, value);
        }
        private DateTime content;

        public DateTime Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        [RelayCommand]
        public async Task CreateContent()
        {
            var res = await apiHelper.CreateInvoiceContent(token, content, username);
            if(res.Successed==true)
            {
                Response = "Successed";
                var message =new OnFinishedOperationToloadData(res.Respnse);
                Messenger.Send(message);
            }
            else
            {
                Response = res.Respnse;
            }
        }
        public async void GetDataFromLogin(string user , string token)
        {
            await SetUserNameAndToken(user, token);
        }
        public async Task SetUserNameAndToken(string username, string token)
        {
            Username = username;
            Token = token;
            await Task.CompletedTask;
        }
    }
}
