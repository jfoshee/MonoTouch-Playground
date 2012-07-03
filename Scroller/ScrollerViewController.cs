using System.Drawing;
using MonoTouch.UIKit;

namespace Scroller
{
    class ScrollViewDelegate : UIScrollViewDelegate
    {
        public UIView ZoomView { get; set; }

        public override UIView ViewForZoomingInScrollView(UIScrollView scrollView)
        {
            return ZoomView;
        }

        public override void ZoomingEnded(UIScrollView scrollView, UIView withView, float atScale)
        {
        }
    }

    public partial class ScrollerViewController : UIViewController
    {
        void SetupViews()
        {
            var bigRectangle = new RectangleF(0, 0, 10000, 10000);
            var subView = new UIView(bigRectangle) { BackgroundColor = UIColor.ScrollViewTexturedBackgroundColor };
//            var greenBox = new UIView(new RectangleF(0, 0, 100, 100)) { BackgroundColor = UIColor.Green };
            var zoomView = subView; //new UIView(bigRectangle) { BackgroundColor = UIColor.Red };
            var scrollDelegate = new ScrollViewDelegate { ZoomView = zoomView };
            var scrollView = new UIScrollView(View.Bounds)
            {
                Delegate = scrollDelegate,
                ContentSize = bigRectangle.Size,
                MaximumZoomScale = 10,
                MinimumZoomScale = 0.1f,
                AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight,
            };
            scrollView.AddSubview(subView);
//            scrollView.AddSubview(greenBox);
            View.Add(scrollView);
        }

        #region boilerplate
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public ScrollerViewController()
			: base (UserInterfaceIdiomIsPhone ? "ScrollerViewController_iPhone" : "ScrollerViewController_iPad", null)
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
            SetupViews();
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

