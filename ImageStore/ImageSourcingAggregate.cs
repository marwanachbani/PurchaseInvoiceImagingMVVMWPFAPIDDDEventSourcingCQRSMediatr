using Core;
using Core.Events;
using ImageStore.Commands;
using MediatR;

namespace ImageStore
{
    public class ImageSourcingAggregate : BaseAggregate
    {
        private Guid _id;
        private byte[] _imageData;
        private string _userName;
        private DateTime _occuredAt;
        private Guid _content;

        public ImageSourcingAggregate(IMediator mediator) : base(mediator)
        {

        }
        public Guid Id { get => _id; set => _id = value; }
        public byte[] ImageData { get => _imageData; set => _imageData = value; }
        public string UserName { get => _userName; set => _userName = value; }
        public Guid Content { get => _content; set => _content = value; }
        public DateTime OccuredAt { get => _occuredAt; set => _occuredAt = value; }
        //-----------------------------------------------AddImage------------------------------------------
        public async Task<BaseResponse>AddImage(Guid id, byte[] imageData, string username, Guid content)
        {
            await SetAddImageParameters(id, imageData, username, content);
            var command = await SendAddImageCommand(_id, _imageData );
            if(command.Successed == false)
            {
                await NotifyAddImageFailed(_id, _userName, DateTime.Now, command.Response);
            }
            if(command.Successed == true)
            {
                await NotifyImageAdded(_id, _imageData, _userName, _content, DateTime.Now,command.Response);
                
            }
            return command;
        }
        public async Task SetAddImageParameters(Guid id , byte[] imageData , string username ,Guid content)
        {
            _id = id;
            _imageData = imageData;
            _userName = username;
            _content = content;
            _occuredAt = DateTime.Now;
            await Task.CompletedTask;
        }
        public async Task NotifyImageAdded(Guid id, byte[] imageData, string username, Guid content , DateTime date ,string response)
        {
            this.PublishEvent(new ImageAdded
            {
                Id = id,
                Aggregate = "ImageSourcing",
                CommittedBy = username,
                ImageData  = imageData,
                Content = content,
                Date = date, 
                Response = response
            });
            this.PublishEvent(new ImageAddForInvoice
            {
                Id = id,
                Aggregate = "ImageSourcing",
                CommittedBy = username,
                ImageData = imageData,
                Content = content,
                Date = date,
                Response = response
            });
            await Task.CompletedTask;
        }
        public async Task NotifyAddImageFailed(Guid id , string username,  DateTime date , string response)
        {
            this.PublishEvent(new AddImageFailed
            {
                Id = id,
                Aggregate = "ImageSourcing",
                CommittedBy = username,
                Date = date, 
                Response = response
            });
            await Task.CompletedTask;
        }
        public async Task<BaseResponse>SendAddImageCommand(Guid id, byte[] imageData)
        {
            var command = await this.SendCommand(new AddImage { Id = id , ImageData = imageData});
            return command;
        }
        public async Task<BaseResponse> UpdateImage(Guid id, byte[] newImageData ,string user)
        {
            await SetUpdateImageParameters(id, newImageData,user);
            var command = await SendUpdateImageCommand(_id, _imageData);

            if (command.Successed == false)
            {
                await NotifyUpdateImageFailed(_id, _userName, DateTime.Now, command.Response);
            }
            else if (command.Successed == true)
            {
                await NotifyImageUpdated(_id, _imageData, _userName, _content, DateTime.Now, command.Response);
            }

            return command;
        }

        public async Task SetUpdateImageParameters(Guid id, byte[] newImageData,string user)
        {
            _id = id;
            _imageData = newImageData;
            _occuredAt = DateTime.Now;
            _userName = user;
            await Task.CompletedTask;
        }

        public async Task NotifyImageUpdated(Guid id, byte[] imageData, string username, Guid content, DateTime date, string response)
        {
            this.PublishEvent(new ImageUpdated
            {
                Id = id,
                Aggregate = "ImageSourcing",
                CommittedBy = username,
                
                Date = date,
                Response = response
            });

            
            await Task.CompletedTask;
        }

        public async Task NotifyUpdateImageFailed(Guid id, string username, DateTime date, string response)
        {
            this.PublishEvent(new UpdateImageFailed
            {
                Id = id,
                Aggregate = "ImageSourcing",
                CommittedBy = username,
                Date = date,
                Response = response
            });

           

            await Task.CompletedTask;
        }

        public async Task<BaseResponse> SendUpdateImageCommand(Guid id, byte[] newImageData)
        {
            var command = await this.SendCommand(new UpdateImage { Id = id, ImageData = newImageData });
            return command;
        }
    }
}