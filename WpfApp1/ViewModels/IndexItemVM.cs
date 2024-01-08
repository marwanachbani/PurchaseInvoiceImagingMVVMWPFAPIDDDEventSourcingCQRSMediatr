using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIApi;
using UIApi.Models;

namespace WpfApp1.ViewModels
{
    public partial class IndexItemVM : ObservableRecipient
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
        private string productName;
        public string ProductName
        {
            get => productName;
            private set => SetProperty(ref productName, value);
        }
        private decimal price;
        public decimal Price
        {
            get => price;
            private set => SetProperty(ref price, value);
        }
        private int quantity;
        public int Quantity
        {
            get => quantity;
            private set => SetProperty(ref quantity, value);
        }
        private Guid id;
        public Guid Id
        {
            get => id;
            private set => SetProperty(ref id, value);
        }
        private ObservableCollection<AddItems> items;
        public ObservableCollection<AddItems> Items
        {
            get => items;
            private set => SetProperty(ref items, value);
        }
        [RelayCommand]
        public  void AddItemsOnTable()
        {
            Items.Add(new AddItems { ProductName = ProductName, Price = Price,Quantity=Quantity});
        }
        [RelayCommand]
        public async Task AddItems()
        {
            var res = await apiHelper.AddItems(Id,Items,Token,Username);
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
