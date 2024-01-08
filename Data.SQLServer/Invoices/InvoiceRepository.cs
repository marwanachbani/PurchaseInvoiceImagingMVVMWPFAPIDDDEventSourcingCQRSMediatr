using Azure;
using Data.SQLServer.Exceptions;
using Data.SQLServer.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;


namespace Data.SQLServer.Invoices
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDbResult _result;
        private readonly IDbContextException _exception;

        public InvoiceRepository(AppDbContext appDbContext, IDbResult result, IDbContextException exception)
        {
            _appDbContext = appDbContext;
            _result = result;
            _exception = exception;
        }

        public async Task<BaseSqlResponse> AddInvoice(Guid id,  Guid contentId, string scannedBy)
        {
            var res = new BaseSqlResponse(); 
            await _appDbContext.Invoices.AddAsync(new Invoice { Id = id , ScannedAt = DateTime.Now, 
                InvoiceContentId = contentId, ScannedBy = scannedBy , 
                LastScanBy = scannedBy , LastScanUpdate = DateTime.Now , LastUpdateBy = scannedBy });
            try
            {
                await _appDbContext.SaveChangesAsync();
   
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();

            }
            catch (DbUpdateException ex )
            {
                var result = _exception.Hundle(ex);
                res.Successed = false; 
                res.Response = result;
                return res; 
            }
            
            
           var response= _result.GetSuccedAddedResult("the image  ");
            res.Successed = true;
            res.Response = response;
            return res; 
           
        }

        public async Task<BaseSqlResponse> ReAddInvoice(Guid invoiceId, string scannedBy)
        {
            var entity = await _appDbContext.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
            var res = new BaseSqlResponse();
            
            entity.LastScanBy = scannedBy;
            entity.LastScanUpdate = DateTime.Now;
            entity.LastUpdateBy = scannedBy;
            entity.Version++; 
            try
            {
                await _appDbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();


            }
            catch (DbUpdateException ex)
            {
                var response = _exception.Hundle(ex);
                res.Successed = false;
                res.Response = response;
                return res;
            }
            var result =  _result.GetModifiedSuccedResult("the image  ");
            res.Successed = true;
            res.Response = result;
            return res;
        }
        public async Task<BaseSqlResponse>ChangeContent(Guid id , Guid contentId , string updatedBy)
        {
            var entity = await _appDbContext.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            var res = new BaseSqlResponse();
            entity.InvoiceContentId = contentId;
            entity.LastUpdateBy = updatedBy;
            entity.Version++;
            try
            {
               
                await _appDbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                var response = _exception.Hundle(ex);
                res.Successed = false;
                res.Response = response;
                return res;
            }
            var result = _result.GetModifiedSuccedResult("the image  ");
            res.Successed = true;
            res.Response = result;
            return res;
        }

        public async Task<BaseSqlResponse> DeleteInvoice(Guid id)
        {
            var entity = await _appDbContext.Invoices.FirstOrDefaultAsync(x => x.Id == id);
            var res = new BaseSqlResponse();
             _appDbContext.Invoices.Remove(entity);
            try
            {
                await _appDbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                var response = _exception.Hundle(ex);
                res.Successed = false;
                res.Response = response;
                return res;
            }
            var result = _result.GetSuccedDeleteResult("the image  ");
            res.Successed = true;
            res.Response = result;
            return res;
        }

        public async Task<BaseSqlResponse> AddInvoiceNumber(Guid invoiceId, string invoiceNumber)
        {
            var entity = await _appDbContext.Invoices.FirstOrDefaultAsync(x => x.Id == invoiceId);
            var res = new BaseSqlResponse();

            
            entity.LastScanUpdate = DateTime.Now;
            entity.InvoiceNumber = invoiceNumber;
            entity.Version++;
            try
            {
                await _appDbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();


            }
            catch (DbUpdateException ex)
            {
                var response = _exception.Hundle(ex);
                res.Successed = false;
                res.Response = response;
                return res;
            }
            var result = _result.GetModifiedSuccedResult("the image  ");
            res.Successed = true;
            res.Response = result;
            return res;
        }
    }
}
