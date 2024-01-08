using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp1.Views;

namespace WpfApp1.ViewModels
{
    public partial class MainViewModel : ObservableRecipient
    {
        private readonly LoginView _loginView = new LoginView();
        private readonly AppZone _zone = new AppZone();
        private UserControl currentControl;
        public UserControl CurrentControl
        {
            get => currentControl;
            private set => SetProperty(ref currentControl, value);
        }


        
        
        public MainViewModel()
        { 
            
            CurrentControl = _loginView;

            Messenger.Register<MainViewModel, UserLogedIn>(this, (r, m) =>
            OnUpdate(m.Value.Control));

        }
        protected override void OnActivated()
        {
        }
        private void OnUpdate(UserControl control)
        {
            CurrentControl = control;
        }




    }
}
