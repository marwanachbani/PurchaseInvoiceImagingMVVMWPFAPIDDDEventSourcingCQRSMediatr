using PurchaseInvoiceCS.CommandModels;
using System.Collections.ObjectModel;

namespace Server.Commands
{
    public class Additems
    {
        public Guid Id { get; set; }
        public string Committed { get; set; }
        public ObservableCollection<ItemCommandModel> Items { get; set; }

        public Additems(ObservableCollection<ItemCommandModel> items, string committed)
        {
            Items = items;
            Committed = committed;
        }
    }
}
