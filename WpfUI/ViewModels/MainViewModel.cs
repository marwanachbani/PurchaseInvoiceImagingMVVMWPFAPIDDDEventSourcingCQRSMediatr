using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfUI.Messages;
using WpfUI.Views;

namespace WpfUI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private UserControl userControl;
        private readonly LoginViewModel loginViewModel = new LoginViewModel();
        public MainViewModel()
        {
            loginViewModel.LogedMessage += OnLoginHandler;
            UserControl = new LoginView();
        }
        public UserControl UserControl { get { return userControl; } 
            set { userControl = value; OnPropertyChanged(nameof(UserControl));  } }
        public void OnLoginHandler(object s, LogedMessage message)
        {
            userControl = new AppZone();
        }
    }
}
