using System;
using SkiaSharp;
using Tesseract;
using TextExtractor.Extensions;
using TextExtractor.Helpers;
using Xamarin.Forms;

namespace TextExtractor
{
    public partial class CroppedImagePreviewPage : ContentPage
    {
        SKBitmap croppedImage = null;

        public CroppedImagePreviewPage(SKBitmap croppedImage)
        {
            InitializeComponent();
            this.croppedImage = croppedImage;

            SetCanvasImage();
        }

        void SetCanvasImage()
        {
            ImageView.Source = SKImageExtensions.SKBitmapToImageSource(croppedImage);
        }

        async void OnDoneButtonClicked(object sender, EventArgs args)
        {
            try
            {
                byte[] imageData = SKImageExtensions.SKBitmapToBytes(croppedImage);
                var textResult = await TesseractHandler.ScanImageData(imageData);
                await DisplayAlert("Result:", textResult, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }

        void MakeItGrayButtonClicked(object sender, EventArgs args)
        {
            try
            {
                croppedImage = ChameleonEngine.GrayScaler(croppedImage);
                SetCanvasImage();
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
