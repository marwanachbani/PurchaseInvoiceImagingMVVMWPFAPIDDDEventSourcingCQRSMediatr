using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class OnImageSelected : ValueChangedMessage<SelectedImage>
    {
        public OnImageSelected(SelectedImage value) : base(value)
        {
        }
    }
    public class SelectedImage
    {
        public SelectedImage(string token, string id)
        {
            Token = token;
            Id = id;
        }

        public string Token { get; set; }
        public string Id { get; set; }
    }
}
