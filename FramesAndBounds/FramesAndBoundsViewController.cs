using System.Drawing;
using MonoTouch.UIKit;

namespace FramesAndBounds
{
    public partial class FramesAndBoundsViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var redView = new UIView(
                new RectangleF(50, 10, 200, 200));
            redView.BackgroundColor = UIColor.Red;
            View.Add(redView);

            var greenView = new UIView(
                new RectangleF(25, 0, 25, 25));
            greenView.BackgroundColor = UIColor.Green;
            redView.Add(greenView);
            // This illustrates that the given frame origin is relative to the parent view
        }

        public FramesAndBoundsViewController() : base ("FramesAndBoundsViewController", null)
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

