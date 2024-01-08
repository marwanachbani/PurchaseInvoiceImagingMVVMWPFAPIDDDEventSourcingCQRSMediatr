using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore.Bridges
{
    public class ImageStoreBridge : IImageStoreBridge
    {
        private readonly IMediator _mediator;

        public ImageStoreBridge(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BaseResponse> AddImage(Guid id, byte[] imageData, string username, Guid content)
        {
            var aggregate = new ImageSourcingAggregate(_mediator); 
            var response = await aggregate.AddImage(id, imageData, username, content);
            return response;
        }

        public async Task<BaseResponse> UpdateImage(Guid id, byte[] newImageData, string user)
        {
            var aggregate = new ImageSourcingAggregate(_mediator);
            var response = await aggregate.UpdateImage(id, newImageData,user);
            return response;
        }
    }
    public interface IImageStoreBridge
    {
        Task<BaseResponse> AddImage(Guid id, byte[] imageData, string username, Guid content);
        Task<BaseResponse> UpdateImage(Guid id, byte[] newImageData, string user);
    }
}
