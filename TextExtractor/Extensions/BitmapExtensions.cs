using System;
using System.IO;
using System.Reflection;
using SkiaSharp;
using TextExtractor.Extensions;
using TextExtractor.Helpers;

/*
 * SKImage SKEncodedOrigin:
 * https://docs.microsoft.com/en-us/dotnet/api/skiasharp.skencodedorigin?view=skiasharp-2.80.2
 */

public static class BitmapExtensions
{
    public static SKBitmap LoadBitmapResource(Type type, string resourceID)
    {
        Assembly assembly = type.GetTypeInfo().Assembly;

        using (Stream stream = assembly.GetManifestResourceStream(resourceID))
        {
            return SKBitmap.Decode(stream);
        }
    }

    public static SKBitmap LoadResourceBitmapFile(string filename)
    {
        var imageBytes = FileHandler.GetResourceFileBytes(filename);
        return SKBitmap.Decode(imageBytes);
    }

    public static SKBitmap LoadFilepathBitmapFile(string filename)
    {
        var imageBytes = FileHandler.GetFilepathFileBytes(filename);
        return SKBitmap.Decode(imageBytes);
    }

    public static void DrawBitmap(this SKCanvas canvas, SKBitmap bitmap, SKRect dest,
                              BitmapStretch stretch,
                              BitmapAlignment horizontal = BitmapAlignment.Center,
                              BitmapAlignment vertical = BitmapAlignment.Center,
                              SKPaint paint = null)
    {
        if (stretch == BitmapStretch.Fill)
        {
            canvas.DrawBitmap(bitmap, dest, paint);
        }
        else
        {
            float scale = 1;

            switch (stretch)
            {
                case BitmapStretch.None:
                    break;

                case BitmapStretch.Uniform:
                    scale = Math.Min(dest.Width / bitmap.Width, dest.Height / bitmap.Height);
                    break;

                case BitmapStretch.UniformToFill:
                    scale = Math.Max(dest.Width / bitmap.Width, dest.Height / bitmap.Height);
                    break;
            }

            SKRect display = CalculateDisplayRect(dest, scale * bitmap.Width, scale * bitmap.Height,
                                                  horizontal, vertical);

            canvas.DrawBitmap(bitmap, display, paint);
        }
    }

    static SKRect CalculateDisplayRect(SKRect dest, float bmpWidth, float bmpHeight,
                                           BitmapAlignment horizontal, BitmapAlignment vertical)
    {
        float x = 0;
        float y = 0;

        switch (horizontal)
        {
            case BitmapAlignment.Center:
                x = (dest.Width - bmpWidth) / 2;
                break;

            case BitmapAlignment.Start:
                break;

            case BitmapAlignment.End:
                x = dest.Width - bmpWidth;
                break;
        }

        switch (vertical)
        {
            case BitmapAlignment.Center:
                y = (dest.Height - bmpHeight) / 2;
                break;

            case BitmapAlignment.Start:
                break;

            case BitmapAlignment.End:
                y = dest.Height - bmpHeight;
                break;
        }

        x += dest.Left;
        y += dest.Top;

        return new SKRect(x, y, x + bmpWidth, y + bmpHeight);
    }

    public enum BitmapStretch
    {
        None,
        Fill,
        Uniform,
        UniformToFill,
        AspectFit = Uniform,
        AspectFill = UniformToFill
    }

    public enum BitmapAlignment
    {
        Start,
        Center,
        End
    }

    public static SKBitmap AutomaticVerticalBitmapRotation(SKBitmap bitmap)
    {
        bool horizontalDominant = bitmap.Width > bitmap.Height;
        SKBitmap rotatedBitmap = horizontalDominant ? RotateBitmap(bitmap, 90.0) : bitmap;

        return rotatedBitmap;
    }

    public static SKBitmap RotateBitmap(SKBitmap bitmap, double angle)
    {
        double radians = Math.PI * angle / 180;
        float sine = (float)Math.Abs(Math.Sin(radians));
        float cosine = (float)Math.Abs(Math.Cos(radians));
        int originalWidth = bitmap.Width;
        int originalHeight = bitmap.Height;
        int rotatedWidth = (int)(cosine * originalWidth + sine * originalHeight);
        int rotatedHeight = (int)(cosine * originalHeight + sine * originalWidth);

        var rotatedBitmap = new SKBitmap(rotatedWidth, rotatedHeight);

        using (var surface = new SKCanvas(rotatedBitmap))
        {
            surface.Translate(rotatedWidth / 2, rotatedHeight / 2);
            surface.RotateDegrees((float)angle);
            surface.Translate(-originalWidth / 2, -originalHeight / 2);
            surface.DrawBitmap(bitmap, new SKPoint());
        }
        return rotatedBitmap;
    }

}
