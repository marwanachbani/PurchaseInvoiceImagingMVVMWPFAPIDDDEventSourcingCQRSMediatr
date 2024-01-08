using Core;
using Data.MongoDb;
using ImageStore.Bridges;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PurchaseInvoiceCS.Bridge;
using PurchaseInvoiceQS;
using PurchaseInvoiceQS.Dtos;
using PurchaseInvoiceQS.Queries;
using Server.Commands;
using Server.Queries;
using Server.Response;
using System.Collections.ObjectModel;

namespace Server.Controllers
{
    [Authorize]
    public class ScannerInvoiceController : BaseController
    {
        private readonly IPurchaseInvoiceCommandBridge _commandBridge;
        private readonly IImageStoreBridge _image; 
        public ScannerInvoiceController(IPurchaseInvoiceCommandBridge commandBridge,IImageStoreBridge image)
        {
            _commandBridge = commandBridge;
     
            _image = image;
        }

        [HttpPost("addinvoice")]
        public async Task<CommandResponse> AddInvoice([FromBody]AddInvoice add)
        {
            var id = Guid.NewGuid();
            var contentId = await PurchaseQueries.GetinvoiceContentId(add.Content);
            var result = await _image.AddImage(id, add.Picture, add.ScannedBy, contentId);
            return new CommandResponse(result.Successed, result.Response);
        }
        [HttpPost("readdinvoice")]
        public async Task<CommandResponse> ReAddInvoice([FromBody] ReAddinvoice re)
        {
            
            var result = await _image.UpdateImage(re.Id, re.ImageData, re.CommittedBy);
            return new CommandResponse(result.Successed, result.Response);
        }
        [HttpPost("createContent")]
        public async Task<CommandResponse> CreateContenent([FromBody] AddInvoiceContent addInvoice)
        {
            var check = await PurchaseQueries.ChechInvoicecontentIfExist(
                addInvoice.Date.Day.ToString()+"/"
                +addInvoice.Date.Month.ToString()+"/"+addInvoice.Date.Year) ;
            if(check == true)
            {
                return new CommandResponse(false, "this content already exist");
            }
            var id = Guid.NewGuid();
            var res = await _commandBridge.CreateContenent(id, addInvoice.Date, addInvoice.CommittedBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("DeleteInvoice")]
        public async Task<CommandResponse> DeleteInvoice([FromBody]DeleteInvoice deletete)
        {
            
            var res = await _commandBridge.DeleteInvoice(deletete.Id, deletete.CommittedBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("DeleteInvoiceContent")]
        public async Task<CommandResponse> DeleteInvoiceContent([FromBody] DeleteInvoiceContentM delete )
        {
            var check = await PurchaseQueries.ChechInvoicecontentIfExist(delete.Content);
            if(check==false)
            {
                return new CommandResponse(false, "this content doesn't exist");
            }
            var id = await PurchaseQueries.GetinvoiceContentId(delete.Content);
            
            var res = await _commandBridge.DeleteInvoiceContent(id, delete.Content , delete.CommittedBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("ModifyInvoiceContentForInvoice")]
        public async Task<CommandResponse> ModifyInvoiceContentForInvoice( Guid contentId, string changeContentBy)
        {
            var id = Guid.NewGuid();
            var res = await _commandBridge.ModifyInvoiceContent(id, contentId, changeContentBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("ModifyInvoiceContent")]
        public async Task<CommandResponse> ModifyInvoiceContent([FromBody]EditInvoiceContent edit)
        {
            var check = await PurchaseQueries.ChechInvoicecontentIfExist(edit.Content);
            if (check == false)
            {
                return new CommandResponse(false, "this content doesn't exist");
            }
            var check2 = await PurchaseQueries.ChechInvoicecontentIfExist(
                edit.Date.Day.ToString() + "/"
                + edit.Date.Month.ToString() + "/" + edit.Date.Year);
            if (check2 == true)
            {
                return new CommandResponse(false, "this content already exist");
            }
            var id = await PurchaseQueries.GetinvoiceContentId(edit.Content);
            var res = await _commandBridge.ModifyInvoiceContent(id,edit.Date,edit.CommittedBy );
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("AddSupplier")]
        public async Task<CommandResponse> AddSupplierHttp([FromBody] AddSupplier add)
        {
            var addinvoicenum = await _commandBridge.AddInvoiceNum(add.InvoiceId, add.InvoiceNumber);
            
            if(addinvoicenum.Successed == true)
            {
                var id = Guid.NewGuid();
                var res = await _commandBridge.AddSupplier(id, add.InvoiceId, add.Supllier, add.Street, add.City, add.State, add.Country, add.ZipCode, add.Committedby);
                return new CommandResponse(res.Successed, res.Response);
            }
            
           return new CommandResponse(addinvoicenum.Successed, addinvoicenum.Response);
        }
        [HttpPost("EditSupplier")]
        public async Task<CommandResponse> EditSupplier([FromBody]EditSupplier edit)
        {
            var res = await _commandBridge.ModifySupplier(edit.Id, edit.InvoiceId, edit.Supllier, edit.Street, edit.City, edit.State, edit.Country, edit.ZipCode, edit.Committedby);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("DeleteSupplier")]
        public async Task<CommandResponse> DeleteSupplier([FromBody]DeleteSupplier delete)
        {
            var res = await _commandBridge.DeleteSupplier(delete.Id, delete.CommmittedBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("AddCalc")]
        public async Task<CommandResponse> AddCalc([FromBody]AddCalc addCalc)
        {
            var id = Guid.NewGuid();
            var res = await _commandBridge.AddInvoiceCalculation(id, addCalc.InvoiceId,addCalc.Subtotal,addCalc.TaxRate,addCalc.TaxAmount,addCalc.RedRate,addCalc.ReductAmunt,addCalc.Total,addCalc.CommittedBy);
            return new CommandResponse(res.Successed,res.Response);
        }
        [HttpPost("ModifyCalc")]
        public async Task<CommandResponse>ModifyCalc( EditCalc edit)
        {
            var res = await _commandBridge.ModifyInvoiceCalculation(edit.InvoiceId, edit.InvoiceId, edit.Subtotal, edit.TaxRate, edit.TaxAmount, edit.RedRate, edit.ReductAmunt, edit.Total, edit.CommittedBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("DeleteCalc")]
        public async Task<CommandResponse>DeleteCalc(DeleteSupplier delete)
        {
            var res = await _commandBridge.DeleteInvoiceCalculation(delete.Id, delete.CommmittedBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("AddItems")]
        public async Task<CommandResponse>AddItems(Additems additems)
        {
            var res = await _commandBridge.AddItems(additems.Id, additems.Items ,additems.Committed);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("ModifyItems")]
        public async Task<CommandResponse> ModifyItems(Additems additems)
        {
            var res = await _commandBridge.ModifyItems(additems.Id, additems.Items, additems.Committed);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpPost("DeleteItems")]
        public async Task<CommandResponse> DeleteItems(DeleteSupplier delete)
        {
            var res = await _commandBridge.DeleteItems(delete.Id, delete.CommmittedBy);
            return new CommandResponse(res.Successed, res.Response);
        }
        [HttpGet]
        public async Task <ObservableCollection<GetAll>>GetAll()
        {
            var result = await PurchaseQueries.GetAllAsync();
            return result;
        }
        [HttpGet("getimagedata")]
        public async Task<byte[]> GetImage(string id)
        {
            var idguid = Guid.Parse(id);
            var query = new ImageDataHelper();
            var result = await query.GetImage(idguid);
            return result;
        }

    }
}
