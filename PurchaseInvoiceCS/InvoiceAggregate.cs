using Core;
using Core.Events;
using Data.SQLServer.Invoices;
using MediatR;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Microsoft.Identity.Client;
using PurchaseInvoiceCS.CommandModels;
using PurchaseInvoiceCS.Commands;
using System.Collections.ObjectModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PurchaseInvoiceCS
{
    public class InvoiceAggregate : BaseAggregate
    {
        private Guid _invoiceId;
        private byte[] _invoiceImage;
        private string _scannedBy;
        private Guid _contentId;
        private string _changeContentBy;
        private string _deleteBy;
        private DateTime _contentDate;
        private string _content;
        private string _supplierName;
        private string _street;
        private string _city;
        private string _state;
        private string _country;
        private string _zip;
        private Guid _id;
        private decimal _subtotal;
        private int _taxRate;
        private decimal _taxAmount;
        private int _reductionRate;
        private decimal _reductionAmount;
        private decimal _total;
        private ObservableCollection<ItemCommandModel> _items;
        

        public Guid ContentId { get => _contentId; set => _contentId = value; }
        public Guid InvoiceId { get => _invoiceId; set => _invoiceId = value; }
        public byte[] InvoiceImage { get => _invoiceImage; set => _invoiceImage = value; }
        public string ScannedBy { get => _scannedBy; set => _scannedBy = value; }
        public DateTime ContentDate { get => _contentDate; set => _contentDate = value; }
        private DateTime OccurredAt { get; set; } = DateTime.Now;
        public string ChangeContentBy { get => _changeContentBy; set => _changeContentBy = value; }
        public string DeleteBy { get => _deleteBy; set => _deleteBy = value; }
        public string Content { get => _content; set => _content = value; }
        public string SupplierName { get => _supplierName; set => _supplierName = value; }
        public string Street { get => _street; set => _street = value; }
        public string City { get => _city; set => _city = value; }
        public string State { get => _state; set => _state = value; }
        public string Country { get => _country; set => _country = value; }
        public string Zip { get => _zip; set => _zip = value; }
        public Guid Id { get => _id; set => _id = value; }
        public decimal Subtotal { get => _subtotal; set => _subtotal = value; }
        public int TaxRate { get => _taxRate; set => _taxRate = value; }
        public decimal TaxAmount { get => _taxAmount; set => _taxAmount = value; }
        public int ReductionRate { get => _reductionRate; set => _reductionRate = value; }
        public decimal ReductionAmount { get => _reductionAmount; set => _reductionAmount = value; }
        public decimal Total { get => _total; set => _total = value; }
        public ObservableCollection<ItemCommandModel> Items { get => _items; set => _items = value; }
        public string InvoiceNumber { get; set; }

        public InvoiceAggregate(IMediator mediator) : base(mediator)
        {   

        }
        //Invoice
        //-----------------------ADD INVOICE----------------------------------------------------------------------------------
        public async Task<BaseResponse> AddInvoice(Guid invoiceId, byte[] invoiceImage , string scannedBy,Guid contentId)
        {
            await SetCommandAddInvoicePropreties(invoiceId,invoiceImage, scannedBy,contentId);
            var response = await SendAddInvoiceCommand(_invoiceId,_invoiceImage,_scannedBy,_contentId);
            if(response.Successed==false)
            {
                await NotifyCreateInvoiceFailed(_invoiceId, _invoiceImage, _scannedBy, OccurredAt, response.Response, _contentId);
                return response;
            }
            await NotifyInvoiceAdeed(_invoiceId, _invoiceImage, _scannedBy,OccurredAt,response.Response ,_contentId);
            return response;
        }
        private async Task<BaseResponse> SendAddInvoiceCommand(Guid invoiceId, byte[] invoiceImage, string scannedBy ,Guid contentId)
        {
            var response = await this.SendCommand(new CreateInvoice { Id =invoiceId,Image=invoiceImage,ScannedBy=scannedBy,ContentId = contentId});
            await Task.CompletedTask;
            return response;
        }
        private async Task NotifyInvoiceAdeed(Guid invoiceId, byte[] invoiceImage, string scannedBy,DateTime date , string response , Guid contentId)
        {
            this.PublishEvent(new InvoiceCreated { Id = invoiceId, Image = invoiceImage, 
                CommittedBy = scannedBy, Date = date ,Response = response , ContentId = contentId }) ;
            await Task.CompletedTask;
        }
        private async Task NotifyCreateInvoiceFailed(Guid invoiceId , byte[] invoiceImage,string scannedBy ,DateTime date,string response , Guid contentId)
        {
            this.PublishEvent(new CreateInvoiceFailed 
            {
                Id = invoiceId,
                Image = invoiceImage,
                CommittedBy = scannedBy,
                Date = date,
                Response = response,
                ContentId = contentId
            });
            await Task.CompletedTask;
        }
        private async Task SetCommandAddInvoicePropreties(Guid invoiceId, byte[] invoiceImage, string scannedBy , Guid contentId)
        {
            _invoiceId = invoiceId;
            _invoiceImage = invoiceImage;
            _scannedBy = scannedBy;
            _contentId = contentId;
            await Task.CompletedTask;
        }
        //-------------------------------------------------------AddInvoiceNumber-----------------------------------------------
        public async Task<BaseResponse>AddInvoiceNum(Guid id , string invoiecNumber)
        {
            return await this.SendCommand(new AddInvoiceNumber { Id = id , InvoiceNumber = invoiecNumber });
        }
        //-------------------------------------------------------------ModifyInvoice----------------------------------------------
        //-------------------------------------------------------------ReAddInvoice-----------------------------------------------
        public async Task<BaseResponse> ReAddInvoice(Guid invoiceId, byte[] invoiceImage, string scannedBy,Guid contentId)
        {
            await SetCommandReAddyInvoicePropreties(invoiceId, invoiceImage, scannedBy, contentId);
            var response = await SendReAddInvoiceCommand(_invoiceId, _invoiceImage, _scannedBy, _contentId);
            if (response.Successed == false)
            {
                await NotifyReAddInvoiceFailed(_invoiceId, _invoiceImage, _scannedBy, OccurredAt, response.Response, _contentId);
                return response;
            }
            await NotifyInvoiceReAdeed(_invoiceId, _invoiceImage, _scannedBy, OccurredAt, response.Response, _contentId);
            return response;
        } 
        private async Task SetCommandReAddyInvoicePropreties(Guid invoiceId, byte[] invoiceImage, string scannedBy, Guid contentId)
        {
            _invoiceId = invoiceId;
            _invoiceImage = invoiceImage;
            _scannedBy = scannedBy;
            _contentId = contentId;
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendReAddInvoiceCommand(Guid invoiceId, byte[] invoiceImage, string scannedBy, Guid contentId)
        {
            var response = await this.SendCommand(new ReAddInvoice 
            {   Id = invoiceId, 
                Image = invoiceImage,
                ScannedBy = scannedBy, 
                ContentId = contentId 
            });
            await Task.CompletedTask;
            return response;
        }
        private async Task NotifyInvoiceReAdeed(Guid invoiceId, byte[] invoiceImage, string scannedBy, DateTime date, string response, Guid contentId)
        {
            this.PublishEvent(new InvoiceCreated
            {
                Id = invoiceId,
                Image = invoiceImage,
                CommittedBy = scannedBy,
                Date = date,
                Response = response,
                ContentId = contentId
            });
            await Task.CompletedTask;
        }
        private async Task NotifyReAddInvoiceFailed(Guid invoiceId, byte[] invoiceImage, string scannedBy, DateTime date, string response, Guid contentId)
        {
            this.PublishEvent(new CreateInvoiceFailed
            {
                Id = invoiceId,
                Image = invoiceImage,
                CommittedBy = scannedBy,
                Date = date,
                Response = response,
                ContentId = contentId
            });
            await Task.CompletedTask;
        }
        //----------------------------------ModifyInvoiceContentId-------------------------------------------------------------------
        public async Task<BaseResponse>ModifyInvoiceContent(Guid id , Guid contentId,string changeContentBy)
        {
            await SetCommandChangeInvoiceContentPropreties(id, changeContentBy, contentId);
            var response = await SendChangeInvoiveContent(_invoiceId, _contentId, _changeContentBy);
            if (response.Successed == false)
            {
                await NotifyChangeInvoiceContentFailed(_invoiceId,  _scannedBy, OccurredAt, response.Response, _contentId);
                return response;
            }
            await NotifyInvoiceContentChanged(_invoiceId, _scannedBy, OccurredAt, response.Response, _contentId);
            return response;
        }
        private async Task SetCommandChangeInvoiceContentPropreties(Guid invoiceId,  string changecontentBy, Guid contentId)
        {
            _invoiceId = invoiceId;
            _changeContentBy = changecontentBy;
            _contentId = contentId;
            await Task.CompletedTask;
        }
        private async Task<BaseResponse>SendChangeInvoiveContent(Guid id, Guid contentId, string changeContentBy)
        {
            var response = await this.SendCommand(new ChangeInvoiceContentForInvice { Id = id, ContentId = contentId,  UpdatedBy = changeContentBy });
            return response; 
        }
        private async Task NotifyInvoiceContentChanged(Guid invoiceId,  string scannedBy, DateTime date, string response, Guid contentId)
        {
            this.PublishEvent(new InvoiceContentForInvoiceChanged
            {
                Id = invoiceId,
                CommittedBy = scannedBy,
                Date = date,
                Response = response,
                ContentId = contentId
            });
            await Task.CompletedTask;
        }
        private async Task NotifyChangeInvoiceContentFailed(Guid invoiceId, string scannedBy, DateTime date, string response, Guid contentId)
        {
            this.PublishEvent(new ChangeInvoiceContentforInvoiceFailed
            {
                Id = invoiceId,
                CommittedBy = scannedBy,
                Date = date,
                Response = response,
                ContentId = contentId
            });
            await Task.CompletedTask;
        }
        //-------------------------------------------------DELETEINVOICE------------------------------------------------
        public async  Task<BaseResponse>DeleteInvoice(Guid invoiceId , string deletedBy)
        {
            await SetDeleteinvoiceParamaters(invoiceId, deletedBy);
            var response = await SendDeleteInvoiceCommand(_invoiceId, _deleteBy);
            if(response.Successed == false)
            {
                await NotifyDeleteInvoiceFailed(_invoiceId,_deleteBy,response.Response,OccurredAt);
                return response;
            }
            await NotifyInvoiceDeleted(_invoiceId, _deleteBy, response.Response, OccurredAt);
            return response; 
        }
        private async Task SetDeleteinvoiceParamaters(Guid id , string deletedBy)
        {
            _invoiceId = id;
            _deleteBy = deletedBy;
            await Task.CompletedTask;
        }
        private async Task NotifyInvoiceDeleted(Guid id , string deleteBy , string response,DateTime date)
        {
            this.PublishEvent(new InvoiceDeleted
            {
                Id = id,
                Date = date,
                Response = response,
                CommittedBy = deleteBy,
                
            }) ;
            await Task.CompletedTask;
        }
        private async Task NotifyDeleteInvoiceFailed(Guid id, string deleteBy, string response,DateTime date)
        {
            this.PublishEvent(new DeleteInvoiceFailed
            {
                Id = id,
                Date = date,
                Response = response,
                CommittedBy = deleteBy,
            });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse>SendDeleteInvoiceCommand(Guid id , string deleteby)
        {
            var response = await this.SendCommand(new DeleteInvoice
            {
                Id = id, DeletedBy=deleteby
            });
            return response;
        }
        //------------------------------------------------INVOICECONTENT------------------------------------------------
        //------------------------------------------------CreateInvoiceContent----------------------------------------
        public async Task<BaseResponse>CreateContenent(Guid id ,DateTime date , string committedBy)
        {
            await SetCreateContentParamaters(id,date, committedBy);
            var response = await SendCreateInvoiceContentCommand(_contentId, _contentDate);
            if(response.Successed==false)
            {
                await NoticeCreateContentInvoiceIsFailed(_contentId, _contentDate, OccurredAt , _changeContentBy , response.Response);
                return response;
            }
            await NoticeContentInvoiceIsCreated(_contentId, _contentDate,OccurredAt, _changeContentBy , response.Response);
            return response; 
        }
        private async Task SetCreateContentParamaters(Guid id , DateTime date ,string committedBy )
        {
            _contentId = id;
            _contentDate = date;
            _changeContentBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task NoticeContentInvoiceIsCreated(Guid id,DateTime date , DateTime createAt , string committedBy,string response)
        {
            this.PublishEvent(new ContentInvoiceCreated { Id = id, ContentDate = date, Date = createAt , Aggregate = "Invoice" , CommittedBy = committedBy , Name= "ContentInvoiceCreated",Response = response }) ;
            await Task.CompletedTask; 
        }
        private async Task NoticeCreateContentInvoiceIsFailed(Guid id, DateTime date, DateTime createdAt , string committedBy , string response)
        {
            this.PublishEvent(new CreateInvoiceContentFailed { Id = id, ContentDay = date, Date = OccurredAt, Aggregate = "Invoice", CommittedBy = committedBy , Response = response , Name= "CreateInvoiceContentFailed" });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendCreateInvoiceContentCommand(Guid id , DateTime date)
        {
            var response = await this.SendCommand(new CreateInvoiceContent { Id = id, ContentDate = date, });
            return response; 
        }
        //----------------------------------------------------------------------------------------------------------------
        //-------------------------------------ModifyInvoiceContent-------------------------------------------------------
        public async Task<BaseResponse>ModifyInvoiceContent(Guid id , DateTime date , string changeContentBy)
        {
            await SetModifyContentParameters(id, date, changeContentBy);
            var response = await SendModifyInvoiceContetn(_contentId, _contentDate);
            if (response.Successed == false)
            {
                await NotifyModifyInvoiceContentFailed(_contentId, _contentDate, OccurredAt,_changeContentBy,response.Response);
                return response;
            }
            await NotifyInvoiceContentModified(_contentId, _contentDate, OccurredAt, _changeContentBy,response.Response);
            return response;
        }
        private async Task SetModifyContentParameters(Guid id , DateTime date,string changeBy)
        {
            _contentId = id;
            _contentDate = date;
            _changeContentBy = changeBy;
            await Task.CompletedTask;
        }
        private async Task NotifyInvoiceContentModified(Guid id, DateTime date,DateTime modifiedAt,string changeBy,string response)
        {
            this.PublishEvent(new InvoiceContentModified { Id = id, ContentDate = date, Date = modifiedAt, CommittedBy = changeBy , Aggregate ="Invoice" , Name = "InvoiceContentModified" , Response = response }) ;
            await Task.CompletedTask;
        }
        private async Task NotifyModifyInvoiceContentFailed(Guid id, DateTime date, DateTime modifiedAt , string changeBy,string response)
        {
            this.PublishEvent(new ModifyInvoiceContentFailed { Id = id, ContentDate = date,Date = modifiedAt, CommittedBy = changeBy, Aggregate = "Invoice", Name = "ModifyInvoiceContentFailed", Response = response });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendModifyInvoiceContetn(Guid id , DateTime date)
        {
            var response = await this.SendCommand(new ModifyinvoiceContent { Id = id, ContentDAte = date });
            return response; 
        }
        //---------------------------------------------------------------------------------------------------------
        //-------------------------------------DeleteInvoiceContent------------------------------------------------
        public async Task<BaseResponse> DeleteInvoiceContent(Guid id , string date ,string committedBy )
        {
            await SetDeleteInvoiceContentParameters(id, date,committedBy);
            var response = await SendDeleteInvoiceContentCommand(_contentId);
            if (response.Successed == false)
            {
                await NotifydeleteInvoiceContentfailed(_contentId, _content, OccurredAt, _deleteBy, response.Response);
                return response;
            }
            await NotifyInvoiceContentDelete(_contentId, _content, OccurredAt,_deleteBy,response.Response);
            return response;
        }
        private async Task SetDeleteInvoiceContentParameters(Guid id , string date , string committedBy )
        {
            _contentId = id;
            _content = date;
            _deleteBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task NotifyInvoiceContentDelete(Guid id, string date , DateTime deleteAt,string DeleteBy , string response)
        {
            this.PublishEvent(new InvoiceContentDeleted { Id = id , ContentDate = date , Date = deleteAt, CommittedBy = DeleteBy , Aggregate ="Invoice" , Response = response });
            await Task.CompletedTask;
        }
        private async Task NotifydeleteInvoiceContentfailed(Guid id, string date, DateTime deleteAt , string committedBy , string response )
        {
            this.PublishEvent(new DeleteInvoiceContentFailed { Id = id, ContentDate = date, Date = deleteAt , CommittedBy = committedBy , Aggregate = "Invoice" , Response = response});
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendDeleteInvoiceContentCommand(Guid id )
        {
            var response = await this.SendCommand(new DeleteInvoiceContent { Id = id });
            return response; 
        }
        //-----------------------------------InvoiceSuppulier--------------------------------
        //-----------------------------------AddSupplier-------------------------------------
        public async Task<BaseResponse>AddSupplier(Guid id , Guid invoiceid, string supplier, string street, string city, string state, string country, string zip , string committedby)
        {
            await SetModifySupplierParams(id, invoiceid, supplier, street, city, state, country, zip, committedby);
            var res = await SendAddSupplierCommand(_id, _invoiceId, _supplierName, _street, _city, _state,
                _country, _zip);
            if (res.Successed == false)
            {
                await NotifyAddSuppplierFailed(_invoiceId, _scannedBy);
            }
            else
            {
                await NotifySuplierAdded(_id, _invoiceId, _supplierName, _street, _city, _state,
                _country, _zip, _scannedBy);
            }
            return res;
        } 
        private async Task SetAddInvoiceParams(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip, string committedby)
        {
            _id = id; _invoiceId = invoiceid; _supplierName = supplier; _street = street;
            _city = city; _state = state; _country = country; _zip = zip; _scannedBy = committedby;
            await Task.CompletedTask;
        }
        private async Task NotifySuplierAdded(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip, string committedby)
        {
            this.PublishEvent(new SupplierAdded
            {
                Id = invoiceid,
                Aggregate = "invoice",
                City = city, 
                State = state,
                Street = street,
                Supllier = supplier , 
                Country = country ,
                ZipCode = zip, CommittedBy = committedby

            });
            await Task.CompletedTask;
        }
        private async Task NotifyAddSuppplierFailed(Guid id , string committedBy)
        {
            this.PublishEvent(new AddSupplierFailed { Id = id , CommittedBy = committedBy , Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse>SendAddSupplierCommand(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip)
        {
            
            var res = await this.SendCommand(new AddSupplier { Id = id , InvoiceId = invoiceid , Supllier =supplier , Street =  street , City = city , State = state , Country = country , ZipCode = zip });
            return res;
        }
        //---------------------------------ModifySupplier------------------------------------
        public async Task<BaseResponse>ModifySupplier(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip , string committedBy)
        {
            await SetModifySupplierParams(id,invoiceid, supplier, street, city, state,country, zip, committedBy);
            var res = await SendModifySupplierCommand(_id, _invoiceId, _supplierName, _street, _city, _state,
                _country, _zip);
            if(res.Successed==false)
            {
                await NotifyModifySuppplierFailed(_invoiceId, _scannedBy);
            }
            else
            {
                await NotifySuplierMidified(_id, _invoiceId, _supplierName, _street, _city, _state,
                _country, _zip,_scannedBy);
            }
            return res;
        }
        private async Task SetModifySupplierParams(Guid id,Guid invoiceid, string supplier, string street, string city, string state, string country, string zip, string committedBy)
        {
            _invoiceId = invoiceid; _supplierName = supplier; _street = street; _id = id;
            _city = city; _state = state; _country = country; _zip = zip; _scannedBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task NotifySuplierMidified(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip, string committedby)
        {
            this.PublishEvent(new SupplierModified
            {
                Id = invoiceid,
                Aggregate = "invoice",
                City = city,
                State = state,
                Street = street,
                Supllier = supplier,
                Country = country,
                ZipCode = zip,
                CommittedBy = committedby

            });
            await Task.CompletedTask;
        }
        private async Task NotifyModifySuppplierFailed(Guid id, string committedBy)
        {
            this.PublishEvent(new AddSupplierFailed { Id = id, CommittedBy = committedBy, Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendModifySupplierCommand(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip)
        {

            var res = await this.SendCommand(new ModifySupplier {  InvoiceId = invoiceid, Supllier = supplier, Street = street, City = city, State = state, Country = country, ZipCode = zip });
            return res;
        }
        //----------------------------------DeleteSupplier----------------------------------
        public async Task<BaseResponse>DeleteSupplier(Guid invoiceId , string committedBy)
        {
            await SetDeleteSupplierPArams(invoiceId, committedBy);
            var res = await SendDeleteSupplierCommand(_invoiceId);
            if(res.Successed==false)
            {
                await NotifyDeleteSuppplierFailed(_invoiceId, _scannedBy);
            }
            else
            {
                await NotifySuppplierDeleted(_invoiceId, _scannedBy);
            }
            return res;
        }
        private async Task SetDeleteSupplierPArams(Guid invoiceId , string committedBy)
        {
            _invoiceId  = invoiceId;
            _scannedBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task NotifySuppplierDeleted(Guid id, string committedBy)
        {
            this.PublishEvent(new SupplierDeleted{ Id = id, CommittedBy = committedBy, Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        private async Task NotifyDeleteSuppplierFailed(Guid id, string committedBy)
        {
            this.PublishEvent(new DeleteSupplierFailed { Id = id, CommittedBy = committedBy, Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse>SendDeleteSupplierCommand(Guid id)
        {
            return await this.SendCommand(new DeleteSupplier { Id = id });
        }
        //--------------------------------InvoiceCalculation--------------------------------
        //--------------------------------AddCalculation------------------------------------
        public async Task<BaseResponse>AddInvoiceCalculation(Guid id , Guid invoiceid , decimal subtotal
                , int taxrate , decimal taxamunt, int reductionrate , decimal reductionamount , decimal total , string committedBy)
        {
            await SetAddCalcParams(id, invoiceid, subtotal, taxrate, taxamunt, reductionrate, reductionamount, total, committedBy);
            var res = await SendAddCalcCommand(_id,_invoiceId, _subtotal, _taxRate, _taxAmount, _reductionRate, _reductionAmount, _total);
            if (res.Successed == false)
            {
                await NotifyAddCalcFailed(_invoiceId, _scannedBy);
            }
            else
            {
                await NotifyCaldAdded(_id, _invoiceId, _subtotal, _taxRate, _taxAmount, _reductionRate, _reductionAmount
                    , _total, _scannedBy);
            }
            return new BaseResponse();
        }
        private async Task SetAddCalcParams(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy)
        {
            _id = id ;
            _invoiceId = invoiceid; _reductionAmount = reductionamount; _total = total;
            _subtotal = subtotal; _taxRate = taxrate; _taxAmount = taxamunt; _reductionRate= reductionrate;
            await Task.CompletedTask;
        }
        private async Task NotifyCaldAdded(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy)
        {
            this.PublishEvent(new CalcAdded
            {
                Id = id,
                Aggregate = "invoice",
                CommittedBy = committedBy,
                Subtotal = subtotal,
                InvoiceId = invoiceid,
                RedRate = reductionrate,
                TaxRate = taxrate, 
                TaxAmount = taxamunt , ReductAmunt = reductionamount,
                Total = total,
            });
            await Task.CompletedTask;
        }
        private async Task NotifyAddCalcFailed(Guid id, string committedBy)
        {
            this.PublishEvent(new AddCalcFailed { Id = id, CommittedBy = committedBy, Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendAddCalcCommand(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total)
        {
            var res = await this.SendCommand(new AddCalc
            {
                Id = id,
                InvoiceId = invoiceid ,
                RedRate = reductionrate,
                ReductAmunt = reductionamount,
                TaxRate = taxrate ,
                TaxAmount = taxamunt , 
                Subtotal = subtotal , 
                Total = total
            });
            return res;
        }

        //-------------------------------ModifyCalculation----------------------------------
        public async Task<BaseResponse> ModifyInvoiceCalculation(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total , string committedBy)
        {
            await SetModifyCalcParams(id,invoiceid,subtotal,taxrate,taxamunt,reductionrate,reductionamount,total,committedBy);
            var res = await SendModifyCalcCommand(_invoiceId, _subtotal, _taxRate, _taxAmount, _reductionRate, _reductionAmount, _total);
            if(res.Successed == false)
            {
                await NotifyModifyCalcFailed(_invoiceId, _scannedBy);
            }
            else
            {
                await NotifyCaldModified(_id,_invoiceId,_subtotal,_taxRate,_taxAmount,_reductionRate,_reductionAmount
                    ,_total,_scannedBy);
            }
            return new BaseResponse();
        }
        private async Task SetModifyCalcParams(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy)
        {
            _id = id;
            _invoiceId = invoiceid; _reductionAmount = reductionamount; _total = total;
            _subtotal = subtotal; _taxRate = taxrate; _taxAmount = taxamunt; _reductionRate = reductionrate;
            await Task.CompletedTask;
        }
        private async Task NotifyCaldModified(Guid id, Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total, string committedBy)
        {
            this.PublishEvent(new CalcModfied
            {
                Id = id,
                Aggregate = "invoice",
                CommittedBy = committedBy,
                Subtotal = subtotal,
                InvoiceId = invoiceid,
                RedRate = reductionrate,
                TaxRate = taxrate,
                TaxAmount = taxamunt,
                ReductAmunt = reductionamount,
                Total = total,
            });
            await Task.CompletedTask;
        }
        private async Task NotifyModifyCalcFailed(Guid id, string committedBy)
        {
            this.PublishEvent(new ModifyCalcFailed { Id = id, CommittedBy = committedBy, Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> SendModifyCalcCommand( Guid invoiceid, decimal subtotal
                , int taxrate, decimal taxamunt, int reductionrate, decimal reductionamount, decimal total)
        {
            var res = await this.SendCommand(new ModifyCalc
            {
                
                InvoiceId = invoiceid,
                RedRate = reductionrate,
                ReductAmunt = reductionamount,
                TaxRate = taxrate,
                TaxAmount = taxamunt,
                Subtotal = subtotal,
                Total = total
            });
            return res;
        }
        //-------------------------------DeleteCalculation----------------------------------
        public async Task<BaseResponse> DeleteInvoiceCalculation(Guid id , string committedBy)
        {
            await SetDeleteCalcPArams(id, committedBy);
            var res = await SendDeleteCalCommand(_invoiceId);
            if(res.Successed == false) 
            {
                await NotifyDeleteCalcFailed(_invoiceId, _scannedBy);
            }
            else
            {
                await NotifyCalcDeleted(_invoiceId, _scannedBy);
            }
            return new BaseResponse();
        }
        private async Task SetDeleteCalcPArams(Guid invoiceId, string committedBy)
        {
            _invoiceId = invoiceId;
            _scannedBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task NotifyCalcDeleted(Guid id, string committedBy)
        {
            this.PublishEvent(new CalcDeleted { Id = id, CommittedBy = committedBy, Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        private async Task NotifyDeleteCalcFailed(Guid id, string committedBy)
        {
            this.PublishEvent(new DeleteCalcFailed { Id = id, CommittedBy = committedBy, Aggregate = "Invoice" });
            await Task.CompletedTask;
        }
        public async Task<BaseResponse>SendDeleteCalCommand(Guid id )
        {
            return await SendCommand(new DeleteCalc { InvoiceId = id });
        }
        //-------------------------------InvoiceItems---------------------------------------
        //-------------------------------AddInvoiceItems------------------------------------
        public async Task<BaseResponse>AddItems(Guid id,ObservableCollection<ItemCommandModel> items , string committedBy)
        {
            await SetAddItemsParams(id,items, committedBy);
            var response = await SendAddItems(_items);
            if(response.Successed == false)
            {
                await NotifyAddItemsFailed(_id, _scannedBy);
            }
            if(response.Successed == true)
            {
                await NotifyItemsAdded(_items, _scannedBy);
            }
            return response;
        }
        private async Task SetAddItemsParams(Guid id,ObservableCollection<ItemCommandModel> items, string committedBy)
        {
            _id = Id;
            _items = items;
            _scannedBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task NotifyItemsAdded(ObservableCollection<ItemCommandModel> items, string committedBy)
        {
            foreach (var item in items)
            {
                this.PublishEvent(new ItemsAdded
                {
                    Aggregate = "invoice",
                    CommittedBy = committedBy,
                    Id = item.Id,
                    InvoiceId = item.InvoiceId,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                });
            }
            await Task.CompletedTask;
        }
        private async Task NotifyAddItemsFailed(Guid invoiceid , string committedBy)
        {
            this.PublishEvent(new AddItemFailed {  InvoiceId = invoiceid, CommittedBy = committedBy });
            await Task.CompletedTask;
        }
        private async Task<BaseResponse>SendAddItems(ObservableCollection<ItemCommandModel> items)
        {
            var response = await SendCommand(new AddItems { Items = items });
            return response;
        }
        //---------------------------------ModifyInvoiceItem------------------------------------------------
        public async Task<BaseResponse>ModifyItems(Guid id ,
            ObservableCollection<ItemCommandModel> newItems , string committedBy)
        {
            await SetModifyItemsParams(id,newItems, committedBy);
            return await ModifyItemsCommabd(_id,_items);
        }
        private async Task SetModifyItemsParams(Guid id,ObservableCollection<ItemCommandModel> items, string committedBy)
        {
            _id = id;
            _items = items;
            _scannedBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task<BaseResponse> ModifyItemsCommabd( Guid id , ObservableCollection<ItemCommandModel> items )
        {
            var response = await SendCommand(new ModifyItem { Id = id, Items = items });
            return response;
        }
        //--------------------------------DeleteInvoiceItem------------------------------------------------
        public async Task<BaseResponse>DeleteItems(Guid invoiceId , string committedBy)
        {
            await SetDeleteItemsParams(invoiceId, committedBy);
            return await SendDeleteItemsCommand(_invoiceId);
        }
        private async Task SetDeleteItemsParams(Guid invoiceid , string committedBy)
        {
            _invoiceId = invoiceid;
            _scannedBy = committedBy;
            await Task.CompletedTask;
        }
        private async Task<BaseResponse>SendDeleteItemsCommand(Guid invoiceId)
        {
            var res = await this.SendCommand(new DeleteItem { Id = invoiceId });
            return res;
        }
        
    }
}