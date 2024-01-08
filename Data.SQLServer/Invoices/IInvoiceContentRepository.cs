using Data.SQLServer.Exceptions;
using Data.SQLServer.Results;
using Microsoft.EntityFrameworkCore;

namespace Data.SQLServer.Invoices
{
    public interface IInvoiceContentRepository
    {
        Task<BaseSqlResponse> AddDateInvoice(Guid id,DateTime date);
        Task<BaseSqlResponse> DeleteDateInvoice(Guid id);
        Task<BaseSqlResponse> ModifyDateInvoice(Guid id, DateTime date);
    }
    public class InvoiceContentRepository : IInvoiceContentRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IDbContextException _dbContextException;
        private readonly IDbResult _result;
        public InvoiceContentRepository(AppDbContext appDbContext, IDbContextException dbContextException, IDbResult result)
        {
            _appDbContext = appDbContext;
            _dbContextException = dbContextException;
            _result = result;
        }

        public async Task<BaseSqlResponse> AddDateInvoice(Guid id,DateTime date)
        {
           await _appDbContext.InvoiceContents.AddAsync(new InvoiceContent { Date = date ,Id = id
               , 
               Contenet= date.Date.Day.ToString()+"/"+ date.Date.Month.ToString() + "/"+
           date.Date.Year.ToString() 
           });
           var response = new BaseSqlResponse();
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();
            }

            catch (DbUpdateException e)
            {
                var  expetion = _dbContextException.Hundle(e);
                response.Response = expetion;
                response.Successed = false;
                await Task.CompletedTask;
                return response;
            }

            response.Response = _result.GetSuccedAddedResult(" Invoices Content of  " + date.ToString());
            response.Successed = true;
            await Task.CompletedTask;
            return response;
        }

        public async Task<BaseSqlResponse> DeleteDateInvoice(Guid id)
        {
            var entity = await _appDbContext.InvoiceContents.FirstOrDefaultAsync(x=>x.Id==id);
            var response = new BaseSqlResponse();
             _appDbContext.InvoiceContents.Remove(entity);
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                var expetion = _dbContextException.Hundle(e);
                response.Response = expetion;
                response.Successed = false;
                await Task.CompletedTask;
                return response;
            }
            response.Response = _result.GetSuccedDeleteResult(" Invoices Content of  " );
            response.Successed = true;
            await Task.CompletedTask;
            return response;
        }

        

        public async Task<BaseSqlResponse> ModifyDateInvoice(Guid id, DateTime date)
        {
            var entity = await _appDbContext.InvoiceContents.FirstOrDefaultAsync(x => x.Id == id);
            var response = new BaseSqlResponse();
            entity.Date = date;
            entity.Contenet = date.Date.Day.ToString() + "/" + date.Date.Month.ToString() + "/" +
           date.Date.Year.ToString();
            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                var expetion = _dbContextException.Hundle(e);
                response.Response = expetion;
                response.Successed = false;
                await Task.CompletedTask;
                return response;
            }
            response.Successed = true;
            response.Response = _result.GetModifiedSuccedResult(" Invoices Content of  " + date.ToString());
            await Task.CompletedTask;
            return response;
        }
    }
}