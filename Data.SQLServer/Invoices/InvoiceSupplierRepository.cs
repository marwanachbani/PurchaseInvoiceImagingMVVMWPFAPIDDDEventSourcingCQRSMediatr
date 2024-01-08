using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public class InvoiceSupplierRepository : IInvoiceSupplierRepository
    {
        private readonly AppDbContext _context = new(); 
        public async Task<BaseResponse> AddSupplier(Guid id, Guid invoiceid, string supplier, string street, string city, string state, string country, string zip)
        {
            var response = new BaseResponse();
            try
            {
                _context.Suppliers.Add(new Supplier { Id = id,  InvoiceId = invoiceid , SupplierName = supplier , 
                    Street = street , City = city , State = state , Country = country , ZipCode = zip
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

        public async Task<BaseResponse> DeleteSupplier(Guid invoiceid)
        {
            var response = new BaseResponse();
            var entity = await _context.Suppliers.FirstOrDefaultAsync(x => x.InvoiceId == invoiceid);
            if (entity == null)
            {
                response.Successed = false; response.Response = "this invoice is not found";
            }
            try
            {
                _context.Suppliers.Remove(entity);
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

        public async Task<BaseResponse> UpdateSupplier(Guid invoiceid, string supplier, string street, string city, string state, string country, string zip)
        {
            var response = new BaseResponse();
            var entity = await _context.Suppliers.FirstOrDefaultAsync(x => x.InvoiceId == invoiceid);
            if (entity == null)
            {
                response.Successed = false; response.Response = "this invoice is not found";
            }
            try
            {
                entity.State = state;
                entity.SupplierName = supplier;
                entity.Street = street;
                entity.City = city;
                entity.Country = country;
                entity.ZipCode = zip;
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
    public interface IInvoiceSupplierRepository
    {
        Task<BaseResponse> AddSupplier(Guid id ,Guid invoiceid, string supplier, string street, string city, string state, string country, string zip);
        Task<BaseResponse> UpdateSupplier(Guid invoiceid, string supplier, string street, string city, string state, string country, string zip);
        Task<BaseResponse> DeleteSupplier(Guid invoiceid);
    }
}
