using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace TextToy
{
    public class TextView : UIView
    {
        public TextView (RectangleF frame)
            :base(frame)
        {
            BackgroundColor = UIColor.White;
        }

        public override void Draw(RectangleF rect)
        {
            base.Draw(rect);
            var s = new NSString("Hello");
            s.DrawString(new PointF(20, 20), UIFont.FromName("Helvetica", 14));
        }
    }

    public partial class TextToyViewController : UIViewController
    {
        void SetupView()
        {
            View.Add(new TextView(View.Bounds));
        }


        #region boilerplate
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public TextToyViewController()
			: base (UserInterfaceIdiomIsPhone ? "TextToyViewController_iPhone" : "TextToyViewController_iPad", null)
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
            SetupView();
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
            // Return true for supported orientations
            if (UserInterfaceIdiomIsPhone)
            {
                return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
            } else
            {
                return true;
            }
        }
        #endregion
    }
}

