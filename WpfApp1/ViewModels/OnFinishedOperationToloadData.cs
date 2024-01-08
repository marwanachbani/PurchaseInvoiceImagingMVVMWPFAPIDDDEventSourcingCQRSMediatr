using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    public class OnFinishedOperationToloadData : ValueChangedMessage<string>
    {
        public OnFinishedOperationToloadData(string value) : base(value)
        {
        }
    }
}
