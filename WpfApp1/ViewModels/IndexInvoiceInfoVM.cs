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
    public partial class IndexInvoiceInfoVM : ObservableRecipient 
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
        private string invoiceNumber;
        public string InvoiceNumber
        {
            get => invoiceNumber;
            private set => SetProperty(ref invoiceNumber, value);
        }
        private string supplier;
        public string Supplier
        {
            get => supplier;
            private set => SetProperty(ref supplier, value);
        }
        private string street;
        public string Street
        {
            get => street;
            private set => SetProperty(ref street, value);
        }
        private string city;
        public string City
        {
            get => city;
            private set => SetProperty(ref city, value);
        }
        private string state;
        public string State
        {
            get => state;
            private set => SetProperty(ref state, value);
        }
        private string country;
        public string Country
        {
            get => country;
            private set => SetProperty(ref country, value);
        }
        private string zip;
        public string Zip
        {
            get => zip;
            private set => SetProperty(ref zip, value);
        }
        private Guid id;
        public Guid Id
        {
            get => id;
            private set => SetProperty(ref id, value);
        }
        [RelayCommand]
        public async Task ReAdd()
        {
            var res = await apiHelper.AddSupplier(Id, InvoiceNumber, Supplier, Street, City, State, Country, Zip, Username,Token) ;
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
