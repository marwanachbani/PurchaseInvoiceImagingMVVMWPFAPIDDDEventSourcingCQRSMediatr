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
    public partial class DeleteContentVM : ObservableRecipient
    {
        private readonly ApiHelper apiHelper = new ApiHelper();
        private string content;
        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        private string token;
        public string Token
        {
            get => token;
            set => SetProperty(ref token, value);
        }
        private string userName;
        public string Username
        {
            get => userName;
            set => SetProperty(ref userName, value);
        }
        private string response;
        public string Response
        {
            get => response;
            set => SetProperty(ref response, value);
        }
        [RelayCommand]
        public async Task EditContent()
        {
            var res = await apiHelper.DeleteInvoiceContent(Token, Content, Username);
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
