using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Scroller2
{
    public partial class Scroller2ViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var zoomingView = new UIView(Rectangle.FromLTRB(100, 200, 500, 5000)) { BackgroundColor = UIColor.Green };
            var redView = new UIView(RectangleF.FromLTRB(10, 10, 20, 20)) { BackgroundColor = UIColor.Red };
            zoomingView.Add(redView);

            var scrollView = new UIScrollView(View.Bounds)
            {
                ViewForZoomingInScrollView = (sv) => zoomingView,
                ContentSize = zoomingView.Bounds.Size,
                ContentOffset = zoomingView.Frame.Location,
                MinimumZoomScale = 0.5f,
                MaximumZoomScale = 2,
                AutoresizingMask = UIViewAutoresizing.FlexibleDimensions,
                ShowsHorizontalScrollIndicator = true,
            };
            scrollView.AddSubview(zoomingView);
            View.Add(scrollView);
        }

        public Scroller2ViewController() : base ("Scroller2ViewController", null)
        {
        }
		
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
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
            return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
        }
    }
}

