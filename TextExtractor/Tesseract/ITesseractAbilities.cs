using System.Threading.Tasks;

namespace Tesseract
{
    public interface ITesseractAbilities
    {
        void JustLogTest();
        Task<string> ScanImage(string imagePath);
        Task<string> ScanImageData(byte[] imageData);
    }
}
