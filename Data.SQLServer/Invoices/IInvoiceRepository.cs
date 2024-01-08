using Data.SQLServer.Results;

namespace Data.SQLServer.Invoices
{
    public interface IInvoiceRepository
    {
        Task<BaseSqlResponse> AddInvoice(Guid id,Guid contentId, string scannedBy); 
        Task<BaseSqlResponse> ReAddInvoice(Guid invoiceId , string scannedBy);
        Task<BaseSqlResponse> ChangeContent(Guid id, Guid contentId, string updatedBy);
        Task<BaseSqlResponse> DeleteInvoice(Guid id);
        Task<BaseSqlResponse> AddInvoiceNumber(Guid invoiceId , string invoiceNumber);

    }
    
}
