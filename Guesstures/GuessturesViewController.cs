using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Guesstures
{
    public partial class GuessturesViewController : UIViewController
    {
        UIPanGestureRecognizer _panRecognizer = new UIPanGestureRecognizer();
        UIPinchGestureRecognizer _pinchRecognizer = new UIPinchGestureRecognizer();

        void DidPan()
        {
            var translation = _panRecognizer.TranslationInView(View);
            Console.WriteLine("Pan translation: {0}", translation);
        }

        void DidPinch()
        {
            var scale = _pinchRecognizer.Scale;
            var velocity = _pinchRecognizer.Velocity;
            Console.WriteLine("Pinch scale: {0}, velocity: {1}", scale, velocity);
        }

        void SetupViews()
        {
            _panRecognizer.AddTarget(DidPan);
            View.AddGestureRecognizer(_panRecognizer);
            _pinchRecognizer.AddTarget(DidPinch);
            View.AddGestureRecognizer(_pinchRecognizer);
        }

        #region boilerplate
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public GuessturesViewController()
			: base (UserInterfaceIdiomIsPhone ? "GuessturesViewController_iPhone" : "GuessturesViewController_iPad", null)
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

