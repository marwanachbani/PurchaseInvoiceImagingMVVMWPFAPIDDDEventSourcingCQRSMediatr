using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIApi;
using CommunityToolkit.Mvvm.Messaging;
using UIApi.Responses;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public partial class ImageViewModel : ObservableRecipient
    {
        public ImageViewModel()
        {
            Messenger.Register<ImageViewModel, OnImageSelected>(this, (r, m) =>
            {
                GetImage(m.Value.Id,m.Value.Token);
            });
        }
        private ApiHelper apiHelper = new();
        private byte[] _imageBytes;
        private double _selectionX;
        public double SelectionX
        {
            get { return _selectionX; }
            set
            {
                _selectionX = value;
                OnPropertyChanged(nameof(SelectionX));
            }
        }

        private double _selectionY;
        public double SelectionY
        {
            get { return _selectionY; }
            set
            {
                _selectionY = value;
                OnPropertyChanged(nameof(SelectionY));
            }
        }

        private double _selectionWidth;
        public double SelectionWidth
        {
            get { return _selectionWidth; }
            set
            {
                _selectionWidth = value;
                OnPropertyChanged(nameof(SelectionWidth));
            }
        }

        private double _selectionHeight;
        public double SelectionHeight
        {
            get { return _selectionHeight; }
            set
            {
                _selectionHeight = value;
                OnPropertyChanged(nameof(SelectionHeight));
            }
        }

        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set
            {
                _imageBytes = value;
                OnPropertyChanged(nameof(ImageBytes));
            }
        }
        private byte[] selectedBytes;
        public byte[] SelectedByte
        {
            get { return selectedBytes; }
            set
            {
                selectedBytes = value;
                OnPropertyChanged(nameof(SelectedByte));
            }
        }

        public async void GetImage(string id , string token)
        {
            await GetImageTask(id, token);
        }
        private BitmapImage _selectedImage;

        public BitmapImage SelectedImage
        {
            get => _selectedImage;
            set => SetProperty(ref _selectedImage, value);
        }



        
        public void SelectImage()
        {
            // Convert the selected image to a byte array
            if (SelectionWidth > 0 && SelectionHeight > 0 && SelectedImage != null)
            {
                // Calculate scale factors
                double scaleX = SelectedImage.PixelWidth / SelectedImage.Width;
                double scaleY = SelectedImage.PixelHeight / SelectedImage.Height;

                // Calculate selection coordinates in original image space
                int x = (int)(SelectionX * scaleX);
                int y = (int)(SelectionY * scaleY);
                int width = (int)(SelectionWidth * scaleX);
                int height = (int)(SelectionHeight * scaleY);

                // Create a CroppedBitmap based on the selected region
                CroppedBitmap croppedBitmap = new CroppedBitmap(
                    SelectedImage,
                    new Int32Rect(x, y, width, height));

                // Convert the cropped image to a byte array
                SelectedByte = ConvertImageToByteArray(croppedBitmap); 

                // You can now use the 'SelectedByte' as needed (e.g., save to a database or perform other operations)
            }

            // You can now use the 'selectedImageBytes' as needed (e.g., save to a database or perform other operations)
        }

        private byte[] ConvertImageToByteArray(CroppedBitmap croppedBitmap)
        {
            byte[] result;

            // Convert CroppedBitmap to byte array
            if (croppedBitmap != null)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder(); // You can choose a different encoder based on your image format
                    encoder.Frames.Add(BitmapFrame.Create(croppedBitmap));
                    encoder.Save(stream);
                    result = stream.ToArray();
                }
            }
            else
            {
                result = null;
            }

            return result;
        }

        
        public async Task GetImageTask(string id, string token)
        {
            ImageBytes = await apiHelper.GelImage(token, id);
            await Task.CompletedTask;
        }
    }
}
