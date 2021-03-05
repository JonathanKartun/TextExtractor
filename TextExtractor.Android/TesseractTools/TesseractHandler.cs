using System;
using System.Threading.Tasks;
using Android.Content;
using Tesseract.Droid;

namespace Droid.TesseractTools
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
            Context context = Android.App.Application.Context;
            tessApi = new TesseractApi(context, AssetsDeployment.OncePerVersion);
            var did = await tessApi.Init("eng");
            //var did = await tessApi.Init("equ");
            Console.WriteLine($"Did Init: {(did ? "YES" : "NO")}");
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
