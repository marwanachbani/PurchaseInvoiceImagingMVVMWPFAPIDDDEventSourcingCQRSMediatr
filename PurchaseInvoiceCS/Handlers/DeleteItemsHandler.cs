using Core;
using Data.SQLServer.Invoices;
using MediatR;
using PurchaseInvoiceCS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Handlers
{
    public class DeleteItemsHandler : IRequestHandler<DeleteItem, BaseResponse>
    {
        private readonly IInvoiceItemRepository _item;

        public DeleteItemsHandler(IInvoiceItemRepository item)
        {
            _item = item;
        }

        public async Task<BaseResponse> Handle(DeleteItem request, CancellationToken cancellationToken)
        {
            var res = await _item.RemoveItems(request.Id);
            return res;
        }
    }
}
