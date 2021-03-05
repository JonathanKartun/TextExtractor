using System;

using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using TextExtractor.PhotoCropper;
using static BitmapExtensions;
using TextExtractor.Helpers;

namespace TextExtractor
{
    public partial class MainPage : ContentPage
    {
        PhotoCropperCanvasView photoCropper;
        SKBitmap originalBitmap;
        SKBitmap croppedBitmap;
        int currentRotationIndex = 0;

        Action<SKBitmap> ChooseImageTargetResponseHandler => delegate (SKBitmap result) { ChosenImageResponder(result); };
        Action<Exception> ChooseImageTargetErrorResponseHandler => delegate (Exception error) { ChosenImageErrorResponder(error); };

        public MainPage()
        {
            InitializeComponent();
            SetupCanvas();
        }

        void SetupCanvas()
        {
            SKBitmap bitmap = LoadBitmapResource(GetType(),
                    "TextExtractor.Assets.SwissLotto02.jpg");
            originalBitmap = bitmap;
            photoCropper = new PhotoCropperCanvasView(bitmap);
            canvasViewHost.Children.Add(photoCropper);
        }

        void OnDoneButtonClicked(object sender, EventArgs args)
        {
            croppedBitmap = photoCropper.CroppedBitmap;
            
            SKCanvasView canvasView = new SKCanvasView();
            canvasView.PaintSurface += OnCanvasViewPaintSurface;

            Navigation.PushAsync(new CroppedImagePreviewPage(croppedBitmap));
        }

        void Rotate90_Clicked(object sender, EventArgs e)
        {
            croppedBitmap = RotateBitmap(originalBitmap, (++currentRotationIndex % 4) * 90);
            photoCropper.ResetBitmap(croppedBitmap);
        }

        async void TakePhotoButton_Clicked(object sender, EventArgs args)
        {
            await PhotoAccessHelper.TakePhotoAsync(false, ChooseImageTargetResponseHandler, ChooseImageTargetErrorResponseHandler);
        }

        async void ChoosePhotoButton_Clicked(object sender, EventArgs args)
        {
            await PhotoAccessHelper.TakePhotoAsync(true, ChooseImageTargetResponseHandler, ChooseImageTargetErrorResponseHandler);
        }

        private void ChosenImageResponder(SKBitmap result)
        {
            if (result != null)
            {
                var fixedBitmap = AutomaticVerticalBitmapRotation(result);
                originalBitmap = fixedBitmap;
                croppedBitmap = originalBitmap;
                photoCropper.ResetBitmap(originalBitmap);
            }
        }

        private void ChosenImageErrorResponder(Exception result)
        {
            if (result is UnauthorizedAccessException)
            {
                DisplayAlert("No Access Permission", result.Message, "OK");
            } else
            {
                DisplayAlert("Error", result.Message, "OK");
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();
            canvas.DrawBitmap(croppedBitmap, info.Rect, BitmapStretch.Uniform);
            //canvas.DrawBitmap(croppedBitmap, info.Rect);
        }
    }
}
