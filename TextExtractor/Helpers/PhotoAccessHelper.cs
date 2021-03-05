using System;
using System.Threading.Tasks;
using SkiaSharp;
using TextExtractor.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TextExtractor.Helpers
{
    public class PhotoAccessHelper
    {
        public static async Task TakePhotoAsync(bool isPicking, Action<SKBitmap> block, Action<Exception> errBlock = null)
        {
            try
            {
                FileResult photo;
                SKBitmap resultBitmapData = null;
                if (isPicking)
                    photo = await MediaPicker.PickPhotoAsync();
                else
                    photo = await MediaPicker.CapturePhotoAsync();

                var filePath = await LoadPhotoAsync(photo);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {filePath}");

                if (filePath != null)
                {
                    resultBitmapData = BitmapExtensions.LoadFilepathBitmapFile(filePath);
                }

                block(resultBitmapData);
                return;
            }
            catch (UnauthorizedAccessException noAccessEx)
            {
                if (errBlock != null)
                {
                    errBlock(noAccessEx);
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                if (errBlock != null)
                {
                    errBlock(ex);
                    return;
                }
            }

            block(null);
        }

        public static async Task<string> LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                return null;
            }
            // save the file into local storage
            var newFile = System.IO.Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = System.IO.File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            //PhotoPath = newFile;
            return newFile;
        }
    }
}
