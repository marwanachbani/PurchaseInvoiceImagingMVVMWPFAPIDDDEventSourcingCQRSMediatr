using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class AddItem
    {
        public AddItem(Guid id, string committed, ObservableCollection<AddItems> items)
        {
            Id = id;
            Committed = committed;
            Items = items;
        }

        public Guid Id { get; set; }
        public string Committed { get; set; }
        public ObservableCollection<AddItems> Items { get; set; }
    }
}
