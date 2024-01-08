using Core;
using Data.SQLServer.Invoices;
using MediatR;
using PurchaseInvoiceCS.Commands;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Handlers
{
    public class AddItemsHandler : IRequestHandler<AddItems, BaseResponse>
    {
        private readonly IInvoiceItemRepository _item;

        public AddItemsHandler(IInvoiceItemRepository item)
        {
            _item = item;
        }

        public async Task<BaseResponse> Handle(AddItems request, CancellationToken cancellationToken)
        {
            var list = new ObservableCollection<Item>();
            foreach (var item in request.Items)
            {
                list.Add(new Item
                {
                    Id = item.Id,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    InvoiceId = item.InvoiceId,

                });
            }
            var response =await _item.AddItems(list);
            return response;
        }
    }
}
