using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public  class BaseCommandRequestHandlerException<TRequest, TResponse, TException>  
        : IMediatrException<BaseCommand, string, Exception>
        where TRequest : class
    where TException : Exception
    {
        public  async Task<string> Handle(BaseCommand request, Exception exception, RequestExceptionHandlerState<string> state, CancellationToken cancellationToken)
        {
            state.SetHandled(exception.Message); 
            var response = state.Response;
            await Task.CompletedTask;
            return response; 
            
        }
    }
}
