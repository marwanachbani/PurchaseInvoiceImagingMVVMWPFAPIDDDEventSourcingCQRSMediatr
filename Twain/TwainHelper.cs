using NTwain;
using NTwain.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Twain
{
    public class TwainHelper
    {
        public event EventHandler<MessageByte> MessageSent;
        public event EventHandler<MessageByte> ScanFinished;
        public async Task<List<string>>GetScanners()
        {
            List<string> twainSources = new List<string>();
            var appId =  TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetExecutingAssembly());
            // Initialize NTwain
            TwainSession session = new TwainSession(appId);

            // Open the session
            if (session.Open() == ReturnCode.Success)
            {
                // Enumerate sources
                foreach (var source in session.GetSources())
                {
                    twainSources.Add(source.Name);
                }

                // Close the session
                session.Close();
            }
            else
            {
                // Handle error when opening the session
                return null;
            }
            await Task.CompletedTask;
            return twainSources;
        }
        public async Task<byte[]> Scan(string twainSourceName , string content)
        {
            byte[] scannedImages = null;
            var appId = TWIdentity.CreateFromAssembly(DataGroups.Image, Assembly.GetExecutingAssembly());
            // Initialize NTwain
            TwainSession session = new TwainSession(appId);

            // Open the session
            session.Open();
            
                session.TransferReady += (s, e) =>
                {
                    Debug.Print("TransferReady is a go.");
                };
                session.DataTransferred += (s, e) =>
                {
                    if (e.NativeData != IntPtr.Zero)
                    {
                        // Handle image data
                        if (e.NativeData != IntPtr.Zero)
                        {
                            var stream = e.GetNativeImageStream();
                            if (stream != null)
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    // Save the image to the stream using the desired format (e.g., JPEG)
                                    var image =Image.FromStream(stream);
                                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                                    // Get the byte array from the stream
                                    var arr =ms.ToArray();
                                    scannedImages = arr;
                                    OnMessageSent(new MessageByte(content,arr));
                                }
                                //Save Image to list
                                
                            }
                        }
                    }
                };
                // Find the selected source
                var selectedSource = session.FirstOrDefault(s => s.Name == twainSourceName);

                
                    // Set the selected source
                    session.DefaultSource = selectedSource;

                    // Acquire the image
                    selectedSource.Open();
                    selectedSource.Capabilities.CapDuplexEnabled.SetValue(BoolType.True);

                    // Start Scan
                    selectedSource.Enable(SourceEnableMode.NoUI, false, IntPtr.Zero);

                    //Close Session
                    selectedSource.Close();
 
            await Task.CompletedTask;
            OnScanFinished(new MessageByte(content, scannedImages));
            return scannedImages;
        }
        protected virtual void OnMessageSent(MessageByte e)
        {
            // Check if there are any subscribers before raising the event.
            MessageSent?.Invoke(this, e);
        }
        protected virtual void OnScanFinished(MessageByte e)
        {
            // Check if there are any subscribers before raising the event.
            ScanFinished?.Invoke(this, e);
        }
    }
    
}
