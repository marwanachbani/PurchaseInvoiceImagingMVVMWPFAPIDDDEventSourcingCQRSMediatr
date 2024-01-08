using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using UIApi;
using WpfUI.Messages;

namespace WpfUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ApiHelper api = new ApiHelper();
        public event EventHandler<LogedMessage> LogedMessage;
        private string _userName;
        private string _password;
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }

        public string UserName { get { return _userName; } set { _userName = value; OnPropertyChanged(nameof(UserName)); } }
        public string Password { get { return _password; } set { _password = value; OnPropertyChanged(nameof(Password)); } }

        public ICommand LoginCommand { get; }
        public async void Login()
        {
            if (string.IsNullOrEmpty(_userName)|| string.IsNullOrEmpty(_password))
            {

            }
            var response = await api.Login(UserName, Password);
            if(response.Successed==true)
            {
                var message = new LogedMessage(response.Respnse);
                OnLogin(message);
            }
        }
        public void OnLogin(LogedMessage message)
        {
            LogedMessage?.Invoke(this, message);
        }
    }
}
