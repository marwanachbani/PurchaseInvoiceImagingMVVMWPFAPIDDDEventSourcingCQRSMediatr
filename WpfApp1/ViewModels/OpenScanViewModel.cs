using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using UIApi;
using UIApi.Responses;
using WpfApp1.Services;

namespace WpfApp1.ViewModels
{
    public partial class OpenScanViewModel : ObservableRecipient
    {
        
        private readonly TwainHelper twainHelper = new TwainHelper();
        private readonly ApiHelper apiHelper = new ApiHelper();
        private string username;

        public OpenScanViewModel()
        {

            Messenger.Register<OpenScanViewModel, OnOpenScan>(this, (r, m) =>

            ReactOnOpenScan(m.Value.User, m.Value.Token));
        }
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
        private string content;

        public string Content
        {
            get => content;
            set => SetProperty(ref content, value);
        }
        private string result;

        public string Result
        {
            get => result;
            private  set => SetProperty(ref result, value);
        }
        private string selectedScanner;

        public string SelectedScanner
        {
            get => selectedScanner;
             set => SetProperty(ref selectedScanner, value);
        }
        private ObservableCollection<string> scannerList;

        public ObservableCollection<string> ScannerList
        {
            get => scannerList;
            private set => SetProperty(ref scannerList, value);
        }
        private ObservableCollection<string> contents;

        public ObservableCollection<string> Contents
        {
            get => contents;
            private set => SetProperty(ref contents, value);
        }
        [RelayCommand]
        public async Task Scan()
        {
            var image = twainHelper.ScanImage(SelectedScanner);
            var res = await apiHelper.AddInvoice(Token, image,Content,Username) ;
            Result = res.Respnse;
            if(res.Successed==true)
            {
                var message = new OnFinishedOperationToloadData(res.Respnse);
                Messenger.Send(message);
            }
            await Task.CompletedTask;
        }
        
        public async void ReactOnOpenScan(string userName , string token)
        {
            var contents = await apiHelper.GelAllForPeperPusher(token);
            var contentstrings = new ObservableCollection<string>();
            foreach (var item in contents)
            {
                contentstrings.Add(item.Content);
                await Task.CompletedTask;
            }
            Contents = contentstrings;
            Username = userName;
            Token = token;
            var listdevice = twainHelper.GetWiaDevices();
            ScannerList = listdevice;
        }
    }
}
