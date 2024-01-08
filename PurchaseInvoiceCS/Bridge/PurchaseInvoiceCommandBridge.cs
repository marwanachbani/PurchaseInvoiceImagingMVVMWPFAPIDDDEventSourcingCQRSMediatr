using Core;
using Data.SQLServer.Invoices;
using MediatR;
using PurchaseInvoiceCS.CommandModels;
using PurchaseInvoiceCS.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Bridge
{
    public class PurchaseInvoiceCommandBridge : IPurchaseInvoiceCommandBridge
    {
        private readonly IMediator _mediatr;

        public PurchaseInvoiceCommandBridge(IMediator mediatr)
        {
            _mediatr = mediatr;
        }

        public async Task<BaseResponse> AddInvoice(Guid invoiceId, byte[] invoiceImage, string scannedBy, Guid contentId)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.AddInvoice(invoiceId, invoiceImage, scannedBy, contentId);
            return res;
        }

        public async Task<BaseResponse> AddInvoiceCalculation(Guid id, Guid invoiceid, decimal subtotal, int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.AddInvoiceCalculation(id,invoiceid, subtotal, taxrate, taxamunt, reductionrate, reductionamount, total, committedBy);
            return res;
        }

        public async Task<BaseResponse> AddInvoiceNum(Guid id, string invoiecNumber)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.AddInvoiceNum(id, invoiecNumber);
            return res;
        }

        public async Task<BaseResponse> AddItems(Guid id, ObservableCollection<ItemCommandModel> items, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.AddItems(id,items, committedBy);
            return res;
        }

        public async Task<BaseResponse> AddSupplier(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip, string committedby)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.AddSupplier(id,invoiceid,supplier, street, city, state, country, zip, committedby);
            return res;
        }

        public Task<BaseResponse> CreateContenent(Guid id, DateTime date,string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = aggregate.CreateContenent(id,date ,committedBy);
            return res;
        }

        public Task<BaseResponse> DeleteInvoice(Guid invoiceId, string deletedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = aggregate.DeleteInvoice(invoiceId, deletedBy);
            return res;
        }

        public async Task<BaseResponse> DeleteInvoiceCalculation(Guid id, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.DeleteInvoiceCalculation(id, committedBy);
            return res;
        }

        public async Task<BaseResponse> DeleteInvoiceContent(Guid id, string date , string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.DeleteInvoiceContent(id, date , committedBy);
            return res;
        }

        public async Task<BaseResponse> DeleteItems(Guid invoiceId, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.DeleteItems(invoiceId,committedBy);
            return res;
        }

        public async Task<BaseResponse> DeleteSupplier(Guid invoiceId, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.DeleteSupplier(invoiceId, committedBy);
            return res;
        }

        public async Task<BaseResponse> ModifyInvoiceCalculation(Guid id, Guid invoiceid, decimal subtotal, int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.ModifyInvoiceCalculation(id, invoiceid, subtotal, taxrate, taxamunt, reductionrate, reductionamount, total, committedBy);
            return res;
        }

        public async Task<BaseResponse> ModifyInvoiceContent(Guid id, Guid contentId, string changeContentBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.ModifyInvoiceContent(id, contentId, changeContentBy);
            return res;
        }

        public async Task<BaseResponse> ModifyInvoiceContent(Guid id, DateTime date , string changeContentBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.ModifyInvoiceContent(id, date , changeContentBy);
            return res;
        }

        public async Task<BaseResponse> ModifyItems(Guid id, ObservableCollection<ItemCommandModel> newItems, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.ModifyItems(id,newItems, committedBy);
            return res;
        }

        public async Task<BaseResponse> ModifySupplier(Guid id, Guid invoiceid
            , string supplier, string street, string city, string state
            , string country, string zip, string committedBy)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.ModifySupplier(id , invoiceid , supplier , street , city , state , country , zip , committedBy);
            return res;
        }

        public async Task<BaseResponse> ReAddInvoice(Guid invoiceId, byte[] invoiceImage, string scannedBy, Guid contentId)
        {
            var aggregate = new InvoiceAggregate(_mediatr);
            var res = await aggregate.ReAddInvoice(invoiceId, invoiceImage, scannedBy, contentId);
            return res;
        }
    }
    public interface IPurchaseInvoiceCommandBridge
    {
        Task<BaseResponse> AddInvoice(Guid invoiceId, byte[] invoiceImage, string scannedBy, Guid contentId);
        Task<BaseResponse> ReAddInvoice(Guid invoiceId, byte[] invoiceImage, string scannedBy, Guid contentId);
        Task<BaseResponse> ModifyInvoiceContent(Guid id, Guid contentId, string changeContentBy);
        Task<BaseResponse> DeleteInvoice(Guid invoiceId, string deletedBy);
        Task<BaseResponse> CreateContenent(Guid id, DateTime date, string committedBy);
        Task<BaseResponse> ModifyInvoiceContent(Guid id, DateTime date,string changeContentBy);
        Task<BaseResponse> DeleteInvoiceContent(Guid id, string date , string committedBy);
        Task<BaseResponse> AddSupplier(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip, string committedby);
        Task<BaseResponse> ModifySupplier(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip, string committedBy);
        Task<BaseResponse> DeleteSupplier(Guid invoiceId, string committedBy);
        Task<BaseResponse> AddInvoiceCalculation(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy);
        Task<BaseResponse> ModifyInvoiceCalculation(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy);
        Task<BaseResponse> DeleteInvoiceCalculation(Guid id, string committedBy);
        Task<BaseResponse> AddItems(Guid id, ObservableCollection<ItemCommandModel> items, string committedBy);
        Task<BaseResponse> ModifyItems(Guid id,
            ObservableCollection<ItemCommandModel> newItems, string committedBy);
        Task<BaseResponse> DeleteItems(Guid invoiceId, string committedBy);
        Task<BaseResponse> AddInvoiceNum(Guid id, string invoiecNumber);
    }
}
