
using Core;
using Core.EventSourcing;
using Data.MongoDb;
using EventStore.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore.Handlers
{
    public class AppendEvevntHandler : IRequestHandler<AppendEvent,BaseResponse>
    {
        private EvenStoreHelper _eventRepo = new();
        private readonly IEventStoreServices eventStoreServices;

        public AppendEvevntHandler(IEventStoreServices eventStoreServices)
        {
            this.eventStoreServices = eventStoreServices;
        }

        public async Task<BaseResponse> Handle(AppendEvent request, CancellationToken cancellationToken)
        {
            var data = eventStoreServices.SerializeData(request.Data);
            var id  = request.Id.ToString();
            await _eventRepo.StoreEvent( new Data.MongoDb.Models.EventDocument(request.Id,id , request.Aggregate, request.Name, request.CommittedBy, data, request.Response, request.DateTime));
            return new BaseResponse(); 
        }
    }
}
