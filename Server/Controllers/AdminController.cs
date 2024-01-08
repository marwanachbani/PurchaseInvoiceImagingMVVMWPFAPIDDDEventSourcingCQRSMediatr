using Data.MongoDb.Models;
using EventStore;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class AdminController : BaseController
    {
        private EventSourcingQueries eventQueries = new EventSourcingQueries();
        [HttpGet("geteventsbyid")]
        public async Task<List<EventDocument>>GetEventsById(Guid id)
        {
            var events = await eventQueries.GetEvents(id);
            return events;
        }
    }
}
