//using System;
//namespace TextExtractor.iOS.TesseractTools
//{
//    public class TesseractService
//    {
//        public TesseractService()
//        {
//        }
//    }
//}

using System;
using System.Threading.Tasks;
using iOS.TesseractTools;
using Tesseract;
using TextExtractor.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(TesseractService))]
namespace iOS.TesseractTools
{
    public class TesseractService : ITesseractAbilities
    {
        public void JustLogTest()
        {
            Console.WriteLine("WooooHooooo --> I - O - Motherfuckin S");
        }

        public Task<string> ScanImage(string imagePath)
        {
            AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
            return appDelegate.tessHandler.ScanImage(imagePath);
        }

        public Task<string> ScanImageData(byte[] imageData)
        {
            AppDelegate appDelegate = (AppDelegate)UIApplication.SharedApplication.Delegate;
            return appDelegate.tessHandler.ScanImageData(imageData);
        }
    }
}
