using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfUI.Messages;

namespace WpfUI.ViewModels
{
    public class AppZoneViewModel : BaseViewModel
    {
        private readonly LoginViewModel _loginViewModel = new LoginViewModel();
        private string _token;
        public AppZoneViewModel()
        {
            _loginViewModel.LogedMessage += OnLoginHandler;
        }
        public string Token { get { return _token; } set
            { _token = value; OnPropertyChanged(nameof(Token)); } }
        private void GetToken(string token)
        {
            Token = token;
        }
        public void OnLoginHandler(object s , LogedMessage message)
        {
            GetToken(message.Message);
        }
    }
}
