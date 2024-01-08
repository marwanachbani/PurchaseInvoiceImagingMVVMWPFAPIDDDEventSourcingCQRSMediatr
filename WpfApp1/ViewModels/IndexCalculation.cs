using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using UIApi;
using WIA;

namespace WpfApp1.ViewModels
{
    public partial class IndexCalculation : ObservableRecipient
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
        private decimal subTotal;
        public decimal Subtotal
        {
            get => subTotal;
            private set => SetProperty(ref subTotal, value);
        }
        private int taxRate;
        public int TaxRate
        {
            get => taxRate;
            private set => SetProperty(ref taxRate, value);
        }
        private decimal taxAmnt;
        public decimal TaxAmnt
        {
            get => taxAmnt;
            private set => SetProperty(ref taxAmnt, value);
        }
        private int redcRate;
        public int RedcRate
        {
            get => redcRate;
            private set => SetProperty(ref redcRate, value);
        }
        private decimal rectAmount;
        public decimal RedcAmnt
        {
            get => rectAmount;
            private set => SetProperty(ref rectAmount, value);
        }
        private decimal total;
        public decimal Total
        {
            get => total;
            private set => SetProperty(ref total, value);
        }
        [RelayCommand]
        public async Task AddCalc()
        {
            var res = await apiHelper.AddCalculation(Id,Subtotal,TaxRate,TaxAmnt,RedcRate,RedcAmnt,Total,Token,Username);
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
