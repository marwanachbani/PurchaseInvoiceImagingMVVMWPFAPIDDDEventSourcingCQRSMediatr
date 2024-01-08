using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {
        private readonly AppDbContext _context = new();
        public async Task<BaseResponse> AddItems(ObservableCollection<Item> items)
        {
            var response = new BaseResponse();
            try
            {
               await _context.Items.AddRangeAsync(items);
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

       

        public async Task<BaseResponse> RemoveItems(Guid id)
        {
            var list = await _context.InvoiceCalculations.Where(b => b.InvoiceId == id).ToListAsync();
            var response = new BaseResponse();
            try
            {
                foreach (var item in list) 
                {
                    _context.Remove(item);
                }
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
    public interface IInvoiceItemRepository
    {
        Task<BaseResponse>AddItems(ObservableCollection<Item> items);

        Task<BaseResponse> RemoveItems(Guid id); 
    }
}
