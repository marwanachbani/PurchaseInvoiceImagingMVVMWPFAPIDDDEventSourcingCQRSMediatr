using Data.SQLServer;
using Microsoft.EntityFrameworkCore;
using PurchaseInvoiceQS.Dtos;
using System.Collections.ObjectModel;

namespace PurchaseInvoiceQS
{
    public static class PurchaseQueries
    {
        

        public async static Task<ObservableCollection<GetAll>> GetAllAsync()
        {
            var context = new AppDbContext();
            var result = new ObservableCollection<GetAll>();
            var list = await context.InvoiceContents.Include(b=>b.Invoices).ToListAsync();
            
            foreach (var item in list)
            {
                var invoices = new List<string>();
                foreach (var inv in item.Invoices)
                {
                    invoices.Add(inv.Id.ToString());
                }
                result.Add(new GetAll { Content = item.Contenet , Invoices = invoices}
                    );  
            }
            return result;
        }
        public static async Task<bool>ChechInvoicecontentIfExist(string content)
        {
            var context = new AppDbContext();
            var entity = await context.InvoiceContents.FirstOrDefaultAsync(b=>b.Contenet == content);
            if(entity == null) { return false; }
            return true;
        }
        public async static Task<Guid>GetinvoiceContentId(string content)
        {
            var context = new AppDbContext();
            var entity = await context.InvoiceContents.FirstOrDefaultAsync(b => b.Contenet == content);
            return entity.Id;
        }
        
    }
    
}