using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public interface IInvoiceCalculationRepository
    {
        Task<BaseResponse> AddInvoiceCalc(Guid id ,Guid invoiceid, decimal subtotal, int taxrate, decimal taxamount, int reductionrate, decimal reducamount, decimal total);
        Task<BaseResponse> ModifyInvoiceCalc(Guid invoiceid, decimal subtotal, int taxrate, decimal taxamount, int reductionrate, decimal reducamount, decimal total);
        Task<BaseResponse> DeleteInvoiceCalc(Guid id); 
    }
    public class InvoiceCalculationRepository : IInvoiceCalculationRepository
    {
        private readonly AppDbContext _context = new AppDbContext();
        public async Task<BaseResponse> AddInvoiceCalc(Guid id, Guid invoiceid, decimal subtotal, int taxrate, decimal taxamount, int reductionrate, decimal reducamount, decimal total)
        {
            var response = new BaseResponse();
            try
            {
                _context.InvoiceCalculations.Add(new InvoiceCalculation { Id = id, InvoiceId = invoiceid , 
                    SubTotal = subtotal , ReductionRate = reductionrate , ReductionAmount = reducamount ,
                    TaxRate = taxrate , TaxAmount = taxamount , Total = total
                });
                await _context.SaveChangesAsync();
                response.Response = "Successed"; response.Successed = true; 
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
                response.Response = "Successed"; response.Successed = true;

            }
            catch (DbUpdateException ex)
            {
                response.Response = ex.Message; response.Successed = false;
            }
            return response;
        }

        public async Task<BaseResponse> DeleteInvoiceCalc(Guid id)
        {
            var response = new BaseResponse();
            var entity = await _context.InvoiceCalculations.FirstOrDefaultAsync(x=> x.InvoiceId == id);
            if (entity == null)
            {
                response.Successed = false; response.Response = "this invoice is not found"; 
            }
            try
            {
                _context.InvoiceCalculations.Remove(entity);
                await _context.SaveChangesAsync();
                response.Response = "Successed"; response.Successed = true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
                response.Response = "Successed"; response.Successed = true;

            }
            catch (DbUpdateException ex)
            {
                response.Response = ex.Message; response.Successed = false;
            }
            return response;
        }

        public async Task<BaseResponse> ModifyInvoiceCalc(Guid invoiceid, decimal subtotal, int taxrate, decimal taxamount, int reductionrate, decimal reducamount, decimal total)
        {
            var response = new BaseResponse();
            var entity = await _context.InvoiceCalculations.FirstOrDefaultAsync(x => x.InvoiceId == invoiceid);
            if (entity == null)
            {
                response.Successed = false; response.Response = "this invoice is not found";
            }
            try
            {
                entity.ReductionRate = reductionrate;
                entity.TaxRate = taxrate;
                entity.ReductionAmount = reducamount;
                entity.TaxAmount = taxamount;
                entity.SubTotal = subtotal;
                entity.Total = total;
                await _context.SaveChangesAsync();
                response.Response = "Successed"; response.Successed = true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _context.SaveChangesAsync();
                response.Response = "Successed"; response.Successed = true;

            }
            catch (DbUpdateException ex)
            {
                response.Response = ex.Message; response.Successed = false;
            }
            return response;
        }
    }
}
