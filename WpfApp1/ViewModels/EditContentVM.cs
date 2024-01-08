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
    public partial class EditContentVM : ObservableRecipient
    {
        private readonly ApiHelper apiHelper = new ApiHelper();
        private string content;
        private string username;
        public string Username
        {
            get => username;
            private set => SetProperty(ref username, value);
        }
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        private string response;
        private string token;

        public string Token
        {
            get => token;
            private set => SetProperty(ref token, value);
        }
        public string Response
        {
            get => response;
            set => SetProperty(ref response, value);
        }
        private DateTime newcontent;

        public DateTime NewContent
        {
            get => newcontent;
            set => SetProperty(ref newcontent, value);
        }
        [RelayCommand]
        public async Task EditContent()
        {
            var res = await apiHelper.EditInvoiceContent(Token, Content, NewContent, Username);
            if (res.Successed == true)
            {
                Response = "Successed";
                var message = new OnFinishedOperationToloadData(res.Respnse);
                Messenger.Send(message);
            }
            else
            {
                Response = res.Respnse;
            }
        }

    }
}
