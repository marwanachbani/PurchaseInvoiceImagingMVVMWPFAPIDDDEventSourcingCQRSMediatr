
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

using UIApi;
using WIA;

namespace WpfApp1.Services
{
    public class TwainHelper
    {
        public  ObservableCollection<string> GetWiaDevices()
        {
            var deviceManager = new DeviceManager();
            var devices = deviceManager.DeviceInfos;
            var list = new ObservableCollection<string>();
            foreach (DeviceInfo deviceInfo in devices)
            {

                var dev = deviceInfo.Properties["Name"].get_Value();
                list.Add(dev);
            }
            return list;
        }

        public byte[] ScanImage(string device, bool resizeToA4 = false, int compressionQuality = 80)
        {
            var manager = new DeviceManager();
            DeviceInfo availableScanner = null;
            var name = Guid.NewGuid().ToString();
            var fileName = "C:\\Users\\Public\\Documents\\" + name + ".tiff";

            for (int i = 1; i <= manager.DeviceInfos.Count; i++)
            {
                if (manager.DeviceInfos[i].Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                if (manager.DeviceInfos[i].Properties["Name"].get_Value() == device)
                {
                    availableScanner = manager.DeviceInfos[i];
                    break;
                }
            }

            if (availableScanner == null)
            {
                throw new Exception("Scanner not found.");
            }

            var scanner = availableScanner.Connect();
            var scannerItem = scanner.Items[1];

            // Adjust scan settings if needed
            SetScanSettings(scannerItem);

            var imgFile = (ImageFile)scannerItem.Transfer("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}");

            // Convert to byte array
            var imageData = ConvertImageFileToByteArray(imgFile, fileName);

            // Resize to A4 if requested
            if (resizeToA4)
            {
                imageData = ResizeImageToA4(imageData);
            }

            // Compress the image if requested
            imageData = CompressImage(imageData, compressionQuality);

            return imageData;
        }

        private void SetScanSettings(Item scannerItem)
        {

        }

        private byte[] ConvertImageFileToByteArray(ImageFile imageFile, string fileName)
        {
            // Save the image file
            imageFile.SaveFile(fileName);

            // Read the saved image file into a byte array
            byte[] imageData;
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    imageData = br.ReadBytes((int)fs.Length);
                }
            }

            // Delete the temporary image file
            File.Delete(fileName);

            return imageData;
        }

        private byte[] ResizeImageToA4(byte[] imageData)
        {
            // Resize the image to A4 dimensions (adjust as needed)
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                using (Image originalImage = Image.FromStream(ms))
                {
                    using (Bitmap resizedImage = new Bitmap(originalImage, new System.Drawing.Size(827, 1169))) // A4 size in pixels
                    {
                        using (MemoryStream resizedMs = new MemoryStream())
                        {
                            resizedImage.Save(resizedMs, System.Drawing.Imaging.ImageFormat.Png);
                            return resizedMs.ToArray();
                        }
                    }
                }
            }
        }

        private byte[] CompressImage(byte[] imageData, int quality)
        {
            // Implement compression logic here (e.g., using a library like ImageMagick or a specific compression algorithm)
            // This example uses the built-in .NET JPEG compression with specified quality
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                using (Image originalImage = Image.FromStream(ms))
                {
                    using (MemoryStream compressedMs = new MemoryStream())
                    {
                        originalImage.Save(compressedMs, System.Drawing.Imaging.ImageFormat.Tiff);
                        return compressedMs.ToArray();
                    }
                }
            }
        }
    }
}
