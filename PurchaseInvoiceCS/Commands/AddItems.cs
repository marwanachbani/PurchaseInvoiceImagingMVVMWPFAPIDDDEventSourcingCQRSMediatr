using Core;
using PurchaseInvoiceCS.CommandModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Commands
{
    public class AddItems : BaseCommand
    {
        
        public ObservableCollection<ItemCommandModel> Items { get; set; }
    }
}
