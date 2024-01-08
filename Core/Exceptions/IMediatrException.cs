using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Exceptions
{
    public interface IMediatrException<in TRequest, TResponse, in TException>
    where TRequest : class
    where TException : Exception
    {
        Task<string> Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken);

    }
}
