using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using iOS.TesseractTools;
using UIKit;

namespace TextExtractor.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public TesseractHandler tessHandler;
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App());
            tessHandler = new TesseractHandler();

            return base.FinishedLaunching(app, options);
        }
    }
}
