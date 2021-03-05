using System;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TextExtractor.Tesseract
{
    public class TesseractTools
    {
        public async void LoadImageData()
        {
            //string filepath = "";
            //ImageSource imageSource = null; imageSource = ImageSource.FromFile(filepath);
            //ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

            //Image image = new Image();
            //image.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(new byte[] { }));

            var image = ImageSourceFrom("");
            await FileSystem.OpenAppPackageFileAsync("Assets/TestData/phototest.tif");


            var testImagePath = "Assets/TestData/phototest.tif";

            using (var stream = await FileSystem.OpenAppPackageFileAsync(testImagePath))
            {

            }
        }
        /* Image Source Helpers */

        public Image ImageSourceFrom(string filePath)
        {
            var imageSource = ImageSource.FromFile(filePath);
            return ConvertImageSourceToImage(imageSource);
        }

        public Image ConvertImageSourceToImage(ImageSource imgSource)
        {
            FileImageSource fileImageSource = (FileImageSource)imgSource;
            //fileImageSource
            Image image = new Image();
            image.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(new byte[] { }));

            return image;
        }

        public byte[] ImageSourceToBytes(ImageSource imageSource)
        {
            StreamImageSource streamImageSource = (StreamImageSource)imageSource;
            System.Threading.CancellationToken cancellationToken =
            System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;
            byte[] bytesAvailable = new byte[stream.Length];
            stream.Read(bytesAvailable, 0, bytesAvailable.Length);
            return bytesAvailable;
        }



        public void TesseractDemoCode()
        {

            if (Device.RuntimePlatform == Device.Android)
            {
                //Activi
                //var currentContext = Android.App.Application.Context;
                //Android
                //DevicePlatform.Android
                //DependencyService.

            }
            else if (Device.RuntimePlatform == Device.iOS)
            {

            }

            ////Android
            //TesseractApi api = new TesseractApi(context, AssetsDeployment.OncePerVersion);
            ////iOS
            //TesseractApi api = new TesseractApi();
            //await api.Init("eng");
            //await api.SetImage("image_path");
            //string text = api.Text;


            //        var testImagePath = "Assets/TestData/phototest.tif";

            //        //var testImagePath = "./phototest.tif";
            //        //if (args.Length > 0)
            //        //{
            //            //testImagePath = args[0];
            //        //}

            //        try
            //        {
            //            //using (var engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default))
            //            using (var engine = new TesseractEngine(@"Assets/TrainedData", "eng", EngineMode.Default))
            //            {
            //                using (var img = Pix.LoadFromFile(testImagePath))
            //                {
            //                    using (var page = engine.Process(img))
            //                    {
            //                        var text = page.GetText();
            //                        Console.WriteLine("Mean confidence: {0}", page.GetMeanConfidence());

            //                        Console.WriteLine("Text (GetText): \r\n{0}", text);
            //                        Console.WriteLine("Text (iterator):");
            //                        using (var iter = page.GetIterator())
            //                        {
            //                            iter.Begin();

            //                            do
            //                            {
            //                                do
            //                                {
            //                                    do
            //                                    {
            //                                        do
            //                                        {
            //                                            if (iter.IsAtBeginningOf(PageIteratorLevel.Block))
            //                                            {
            //                                                Console.WriteLine("<BLOCK>");
            //                                            }

            //                                            Console.Write(iter.GetText(PageIteratorLevel.Word));
            //                                            Console.Write(" ");

            //                                            if (iter.IsAtFinalOf(PageIteratorLevel.TextLine, PageIteratorLevel.Word))
            //                                            {
            //                                                Console.WriteLine();
            //                                            }
            //                                        } while (iter.Next(PageIteratorLevel.TextLine, PageIteratorLevel.Word));

            //                                        if (iter.IsAtFinalOf(PageIteratorLevel.Para, PageIteratorLevel.TextLine))
            //                                        {
            //                                            Console.WriteLine();
            //                                        }
            //                                    } while (iter.Next(PageIteratorLevel.Para, PageIteratorLevel.TextLine));
            //                                } while (iter.Next(PageIteratorLevel.Block, PageIteratorLevel.Para));
            //                            } while (iter.Next(PageIteratorLevel.Block));
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        catch (Exception e)
            //        {
            //            //	e	{System.PlatformNotSupportedException: Operation is not supported on this platform.   at System.Reflection.Emit.AssemblyBuilder.DefineDynamicAssembly (System.Reflection.AssemblyName name, System.Reflection.Emit.AssemblyBuilderAccess access) [0x00000] in /Librar…}	
            //            Trace.TraceError(e.ToString());
            //            Console.WriteLine("Unexpected Error: " + e.Message);
            //            Console.WriteLine("Details: ");
            //            Console.WriteLine(e.ToString());
            //        }
            //        Console.Write("Press any key to continue . . . ");
            //        Console.ReadKey(true);
        }
    }
}
