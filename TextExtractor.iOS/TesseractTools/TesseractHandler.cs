using System;
using System.Threading.Tasks;
using Tesseract.iOS;

namespace iOS.TesseractTools
{
    public class TesseractHandler
    {
        TesseractApi tessApi;

        public TesseractHandler()
        {
            SetupTess();
        }

        private async void SetupTess()
        {
            tessApi = new TesseractApi();
            await tessApi.Init("eng");
        }

        public async Task<string> ScanImage(string imagePath)
        {
            await tessApi.SetImage(imagePath);
            string text = tessApi.Text;
            tessApi.Clear();
            return text;
        }

        public async Task<string> ScanImageData(byte[] imageData)
        {
            await tessApi.SetImage(imageData);
            string text = tessApi.Text;
            tessApi.Clear();
            return text;
        }
    }
}
