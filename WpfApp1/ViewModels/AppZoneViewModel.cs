using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using UIApi;
using UIApi.Responses;
using WpfApp1.Views.InfoZoneViews;
using WpfApp1.Views.PictureZneViews;

namespace WpfApp1.ViewModels
{
    public partial class AppZoneViewModel : ObservableRecipient
    {
        private readonly ApiHelper apiHelper= new ApiHelper();
        public AppZoneViewModel()
        {
            Messenger.Register<AppZoneViewModel, UserLogedIn>(this, (r, m) =>

            Connect(m.Value.UserNameMessage, m.Value.ResponseMessage, m.Value.Alls));
            Messenger.Register<AppZoneViewModel, OnFinishedOperationToloadData>(this, (r, m) =>

            GetallDataAfterFinishedOperation(Token));

        }
        private string selectedNode;

        public string SelectedNode
        {
            get => selectedNode;
            set => SetProperty(ref selectedNode, value);
        }
        private bool rescanButton;

        public bool RescanButton
        {
            get => rescanButton;
            set => SetProperty(ref rescanButton, value);
        }
        private bool deleteInvoiceButton;

        public bool DeleteInvoiceButton
        {
            get => deleteInvoiceButton;
            set => SetProperty(ref deleteInvoiceButton, value);
        }
        private bool indexButton;

        public bool IndexButton
        {
            get => indexButton;
            set => SetProperty(ref indexButton, value);
        }
        private string username ;

        public string Username
        {
            get => username;
            private set => SetProperty(ref username, value);
        }
        private UserControl infoZone;

        public UserControl InfoZone
        {
            get => infoZone;
            private set => SetProperty(ref infoZone, value);
        }
        private UserControl imageZone;

        public UserControl ImageZone
        {
            get => imageZone;
            private set => SetProperty(ref imageZone, value);
        }
        private string _selectedItemText;
        public string SelectedItemText
        {
            get { return _selectedItemText; }
            set { SetProperty(ref _selectedItemText, value); }
        }

        private ObservableCollection<All> treeviewList;

        public ObservableCollection<All> TreeViewList
        {
            get => treeviewList;
            private set => SetProperty(ref treeviewList, value);
        }
      
        private string token ;

        public string Token
        {
            get => token;
            private set => SetProperty(ref token, value);
        }

        public async Task GetDataFromLogin(string user ,  string token , ObservableCollection<All> list)
        {
            Username = user;
            Token = token;
            TreeViewList = list;
            await Task.CompletedTask;
        }
        public async void Connect(string user , string tok , ObservableCollection<All>list)
        {
            ImageZone = new ImageView();
            await GetDataFromLogin(user , tok , list);
            
        }
        [RelayCommand]
        public async Task OpenCreateInvoiceContent()
        {
            var message = new UserAndToken(Username,Token);
            var res = new OnOpenCreateContent(message);
            
            InfoZone = new CreateNewContent();
            Messenger.Send(res);
            await Task.CompletedTask; 
        }
        [RelayCommand]
        public async Task OpenCreateScanView()
        {
            var message = new UserAndToken(Username, Token);
            var res = new OnOpenScan(message);
            
            InfoZone = new ScanInvoiceView();
            Messenger.Send(res);
            await Task.CompletedTask;
        }
       
        [RelayCommand]
        public void OnTreeViewItemSelected(string selectedItem)
        {
            var message = new SelectedImage(Token, selectedItem);
            var messagetosend = new OnImageSelected(message);
            Messenger.Send(messagetosend);
            SelectedItemText = selectedItem;
            IndexButton = true;
            RescanButton = true;
            DeleteInvoiceButton = true;
            // Perform additional actions based on the selected item
        } 
        public void OnUnSelectedTreeviewItem()
        {
            IndexButton = false; 
            RescanButton = false;
            DeleteInvoiceButton = false;
        }
        private async  void GetallDataAfterFinishedOperation(string token)
        {
           var list =  await  apiHelper.GelAllForPeperPusher(token);
            TreeViewList = list;
        }
    }
}
