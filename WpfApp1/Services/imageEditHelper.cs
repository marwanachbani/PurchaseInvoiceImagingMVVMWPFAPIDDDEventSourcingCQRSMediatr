using ImageMagick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Services
{
    public class imageEditHelper
    {
        public byte[] EditImage(byte[] imageData)
        {
            using (MagickImage image = new MagickImage(imageData))
            {
                image.Enhance();
                image.Kuwahara();
                image.Normalize();
                image.ReduceNoise();
                image.Rotate(90);
                image.Scale(image.Width, image.Height);
                image.WhiteBalance();
                // Get the edited image data as a byte array
                return image.ToByteArray();
            }
        }
        public byte[] ToGreayScale(byte[] imageData)
        {
            using (MagickImage image = new MagickImage(imageData))
            {
                image.Grayscale();
                
                // Get the edited image data as a byte array
                return image.ToByteArray();
            }
        }
        public byte[] ToAutoOrient(byte[] imageData)
        {
            using (MagickImage image = new MagickImage(imageData))
            {

                image.AutoOrient();
                // Get the edited image data as a byte array
                return image.ToByteArray();
            }
        }
        public byte[] ToRotate(byte[] imageData)
        {
            using (MagickImage image = new MagickImage(imageData))
            {
                image.Rotate(90);
                // Get the edited image data as a byte array
                return image.ToByteArray();
            }
        }
    }
}
