using CommunityToolkit.Mvvm.Messaging.Messages;

namespace WpfApp1.ViewModels
{
    
    public class OnOpenCreateContent : ValueChangedMessage<UserAndToken>
    {
            public OnOpenCreateContent(UserAndToken value) : base(value)
            {
            }
    }
    public class UserAndToken
    {
        public UserAndToken(string user, string token)
        {
            User = user;
            Token = token;
        }

        public string User { get; set; }
        public string Token { get; set; }
    }




}
