using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace HW6_final
{
    class ImageDownload
    {
        public async Task<BitmapImage> DownloadImageTaskAsync(string fileUrl)
        {
            using (WebClient client = new WebClient())
            {
                byte[] data = await client.DownloadDataTaskAsync(fileUrl);
                return ConvertBytesToImage(data);
            }
        }

        public BitmapImage ConvertBytesToImage(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            stream.Seek(0, SeekOrigin.Begin);
            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();

            image.Freeze();
            return image;
        }
    }
}
