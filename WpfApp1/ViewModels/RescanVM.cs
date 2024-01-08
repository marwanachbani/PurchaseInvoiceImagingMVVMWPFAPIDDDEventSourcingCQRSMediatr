using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIApi;

namespace WpfApp1.ViewModels
{
    public partial class RescanVM : ObservableRecipient
    {
        private readonly ApiHelper apiHelper = new ApiHelper();
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
        private Guid id;
        public Guid Id
        {
            get => id;
            private set => SetProperty(ref id, value);
        }
        private byte[] imageData;
        public byte[] ImageData
        {
            get => imageData;
            private set => SetProperty(ref imageData, value);
        }
        public async Task ReAdd()
        {
            var res = await apiHelper.ReAddInvoice(Id, Token, ImageData , Username);
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
