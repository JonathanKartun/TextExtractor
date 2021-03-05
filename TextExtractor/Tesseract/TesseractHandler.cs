using System.Threading.Tasks;
using Xamarin.Forms;

namespace Tesseract
{
    public class TesseractHandler
    {
        private static ITesseractAbilities TessAbility =>
            DependencyService.Resolve<ITesseractAbilities>();

        public static void JustLogTest()
        {
            TessAbility.JustLogTest();
        }

        public static Task<string> ScanImage(string imagePath)
        {
            return TessAbility.ScanImage(imagePath);
        }

        public static Task<string> ScanImageData(byte[] imageData)
        {
            return TessAbility.ScanImageData(imageData);
        }
    }
}
