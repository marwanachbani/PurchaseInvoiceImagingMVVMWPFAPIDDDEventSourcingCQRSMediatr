using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class OnOpenScan : ValueChangedMessage<UserAndToken>
    {
        public OnOpenScan(UserAndToken value) : base(value)
        {
        }
    }
    public class UserAndTokenAndContent
    {
        public UserAndTokenAndContent(string user, string token, string content)
        {
            User = user;
            Token = token;
            Content = content;
        }
        public string Content { get; set; }
        public string User { get; set; }
        public string Token { get; set; }
    }
}
