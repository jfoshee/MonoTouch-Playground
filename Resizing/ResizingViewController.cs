using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Resizing
{
    public partial class ResizingViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public ResizingViewController()
			: base (UserInterfaceIdiomIsPhone ? "ResizingViewController_iPhone" : "ResizingViewController_iPad", null)
        {
        }


		
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.
        }
		
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
			
            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;
			
            ReleaseDesignerOutlets();
        }
		
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
//            // Return true for supported orientations
//            if (UserInterfaceIdiomIsPhone)
//            {
//                return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
//            } else
//            {
//                return true;
//            }
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

            if (InterfaceOrientation == UIInterfaceOrientation.LandscapeLeft || 
                InterfaceOrientation == UIInterfaceOrientation.LandscapeRight)
                MyChildView.Frame = new RectangleF(0, 0, View.Bounds.Width, 20);
            else
                MyChildView.Frame = new RectangleF(0, 0, 20, View.Bounds.Height);
        }
    }
}

