using Core;
using Data.MongoDb;
using ImageStore.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore.Handlers
{

    public class AddImageHandler : IRequestHandler<AddImage, BaseResponse>
    {
        private readonly ImageDataHelper imageData = new();
        public async Task<BaseResponse> Handle(AddImage request, CancellationToken cancellationToken)
        {
            var command = await imageData.AddImage(request.Id, request.ImageData);
            return command; 
        }
    }
}
