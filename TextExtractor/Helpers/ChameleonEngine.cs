using System;
using SkiaSharp;
using TextExtractor.Extensions;

namespace TextExtractor.Helpers
{
    public class ChameleonEngine
    {
        public static SKBitmap GrayScaler(SKBitmap bitmap)//byte[] imageBytes)
        {
            //SKMemoryStream memMan = new SKMemoryStream(imageBytes);
            //SKImage
            //SKBitmap bitmap = SKBitmap.Decode(memMan);
            SKImage image = SKImage.FromBitmap(bitmap);

            var filterGray = SKImageFilter.CreateColorFilter(GrayscaleColorMatrix);

            //Finalize Image
            var imageRect = SKRect.Create(0, 0, image.Width, image.Height);
            var imageRectI = SKRectI.Ceiling(imageRect);
            SKRectI outRect; SKPoint outPoint;

            var finalImage = image.ApplyImageFilter(filterGray, imageRectI, imageRectI, out outRect, out outPoint);
            return SKImageExtensions.SKImageToSKBitmap(finalImage);
        }

        private static SKColorFilter GrayscaleColorMatrix =>
            SKColorFilter.CreateColorMatrix(new float[] {
                        0.21f, 0.72f, 0.07f, 0, 0,
                        0.21f, 0.72f, 0.07f, 0, 0,
                        0.21f, 0.72f, 0.07f, 0, 0,
                        0,     0,     0,     1, 0
                    });
        }
}
