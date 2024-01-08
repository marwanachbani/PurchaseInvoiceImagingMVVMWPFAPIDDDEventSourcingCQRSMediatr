using Azure;
using Core;
using Core.EventSourcing;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Data.SQLServer.Events
{
    public interface IEventStoreRepository
    {
        Task Append(Guid id, string committedBy, BaseEvent @event, string name, DateTime date, string aggregate ,string response ) ;
    }
    public class EventStoreRepository : IEventStoreRepository , IDisposable
    {
        private readonly IEventStoreServices _services;
        private readonly AppDbContext _appDbContext ;

        public EventStoreRepository(IEventStoreServices services, AppDbContext appDbContext )
        {
            _services = services;
            _appDbContext = appDbContext;
        }

        public async Task Append(Guid id , string committedBy,BaseEvent @event , string name ,DateTime date , string aggregate,string response ) 
        {
            var data  =  _services.SerializeData(@event);
            await _appDbContext.Events.AddAsync(new Event { Id = @event.Id , AggregateId = @event.Id , OccuredAt = date , 
                EventName = name , AggregateName = aggregate , CommittedBy = committedBy , Data = data , Response = @event.Response });
            try
            {
                _appDbContext.SaveChanges();
                Dispose();
                
            }
            catch(DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
                
                Dispose();
                
            }
            
        }

        public void Dispose()
        {
            
            _appDbContext.Dispose();
        }
    }
}
