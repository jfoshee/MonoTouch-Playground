using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace TextToy
{
    public class TextView : UIView
    {
        UIPanGestureRecognizer _pan = new UIPanGestureRecognizer();
        UIPinchGestureRecognizer _pinch = new UIPinchGestureRecognizer();
        PointF _point = new PointF(100, 100);
        float _size = 14;

        PointF _panningStartingPoint;
        void OnPan()
        {
            if (_pan.State == UIGestureRecognizerState.Began)
                _panningStartingPoint = _point;
            var d = _pan.TranslationInView(this);
            var p = _panningStartingPoint;
            _point = new PointF(p.X + d.X, p.Y + d.Y);
            SetNeedsDisplayInRect(Bounds);
        }

        float _pinchingStartingSize;
        void OnPinch()
        {
            if (_pinch.State == UIGestureRecognizerState.Began)
                _pinchingStartingSize = _size;
            _size = _pinchingStartingSize * _pinch.Scale;
            SetNeedsDisplayInRect(Bounds);
        }

        public TextView (RectangleF frame)
            :base(frame)
        {
            BackgroundColor = UIColor.White;
            AutoresizingMask = UIViewAutoresizing.All;
            AddGestureRecognizer(_pan);
            AddGestureRecognizer(_pinch);
            _pan.AddTarget(OnPan);
            _pinch.AddTarget(OnPinch);
        }

        public override void Draw(RectangleF rect)
        {
            base.Draw(rect);
            var s = new NSString("Hello");
            s.DrawString(_point, UIFont.FromName("Helvetica", _size));
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

