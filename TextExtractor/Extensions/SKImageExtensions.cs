
using System.IO;
using SkiaSharp;
using Xamarin.Forms;

namespace TextExtractor.Extensions
{
    public class SKImageExtensions
    {
        public static ImageSource SKImageToImageSource(SKImage image)
        {
            Stream stream = SKImageToStream(image);
            return ImageSource.FromStream(() => stream);
        }

        public static SKBitmap SKImageToSKBitmap(SKImage image)
        {
            return SKBitmap.FromImage(image);
        }

        public static SKBitmap BytesToImage(byte[] imageBytes)
        {
            return SKBitmap.Decode(imageBytes);
        }

        public static Stream SKImageToStream(SKImage image)
        {
            SKData encoded = image.Encode();
            return encoded.AsStream();
        }

        public static SKImage SKBitmapToSKImage(SKBitmap bitmap)
        {
            return SKImage.FromBitmap(bitmap);
        }

        public static ImageSource SKBitmapToImageSource(SKBitmap bitmap)
        {
            return SKImageToImageSource(SKImage.FromBitmap(bitmap));
        }

        public static byte[] MemoryStreamToByteArray(MemoryStream sourceStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                sourceStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        public static SKData SKImageToSKData(SKImage image)
        {
            return image.Encode();
        }

        public static byte[] SKImageToBytes(SKImage image)
        {
            Stream imageStream = image.Encode().AsStream();
            using (var memStream = new MemoryStream())
            {
                imageStream.CopyTo(memStream);
                return memStream.ToArray();
            }
        }

        public static byte[] SKBitmapToBytes(SKBitmap bitmap)
        {
            var image = SKBitmapToSKImage(bitmap);
            return SKImageToBytes(image);
        }

        public static SKBitmap MemoryStreamToSKBitmap(MemoryStream sourceStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                sourceStream.CopyTo(memoryStream);
                memoryStream.Position = 0;
                sourceStream.Position = 0;
                return SKBitmap.Decode(memoryStream);
            }
        }
    }
}

