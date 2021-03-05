using System;
using System.Threading.Tasks;
using Droid.TesseractTools;
using Tesseract;
using TextExtractor.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(TessService))]
namespace Droid.TesseractTools
{
    public class TessService : ITesseractAbilities
    {
        public void JustLogTest()
        {
            Console.WriteLine("WooooHooooo --> Android");
        }

        public Task<string> ScanImage(string imagePath)
        {
            MainActivity mainActivity = MainActivity.Context;
            return mainActivity.tessHandler.ScanImage(imagePath);
        }

        public Task<string> ScanImageData(byte[] imageData)
        {
            MainActivity mainActivity = MainActivity.Context;
            return mainActivity.tessHandler.ScanImageData(imageData); 
        }
    }
}