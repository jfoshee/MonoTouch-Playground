using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Collections.Generic;

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

        List<UILabel> _labels = new List<UILabel>();
        List<UIImage> _images = new List<UIImage>();


        public TextView (RectangleF frame)
            :base(frame)
        {
            BackgroundColor = UIColor.White;
            AutoresizingMask = UIViewAutoresizing.All;
            AddGestureRecognizer(_pan);
            AddGestureRecognizer(_pinch);
            _pan.AddTarget(OnPan);
            _pinch.AddTarget(OnPinch);

            for (int i = 0; i < 100; i++) {
                var label = new UILabel();
                _labels.Add(label);
                Add(label);
            }

            for (int i = 0; i < 100; i++) {
                for (int j = 0; j < 10; j++) {
                    var s = String.Format("({0}, {1})", i, j);
                    var img = StringToImage(s, 64);
                    _images.Add(img);
                }
            }

        }
                
        static public UIImage StringToImage(string s, float fontSize)
        {
            return StringToImage(s, UIFont.FromName("Helvetica", fontSize));
        }

        static public UIImage StringToImage(string s, UIFont font)
        {
            var ns = new NSString(s);
            var size = ns.StringSize(font);
            UIGraphics.BeginImageContext(size);
            ns.DrawString(PointF.Empty, font);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            return image;
        }

        void DrawString(ref NSString s, ref PointF p)
        {
            s.DrawString(p, UIFont.FromName("Helvetica", _size));
        }

        void DrawLabel(ref int i, ref int j, ref NSString s, ref PointF p)
        {
            var lbl = _labels[i * 10 + j];
            lbl.Text = s;
            lbl.Font = UIFont.FromName("Helvetica", _size);
            lbl.Frame = new RectangleF(p, new SizeF(100, 40));
            lbl.SizeToFit();
        }

        public override void Draw(RectangleF rect)
        {
            for (int i = 0; i < 10; i++) {
                for (int j = 0; j < 10; j++) {
                    var s = new NSString(String.Format("({0}, {1})", i, j));
                    var p = new PointF(_point.X + i * 3 * _size , _point.Y + j * _size);
//                    DrawString(ref s, ref p);
//                    DrawLabel(ref i, ref j, ref s, ref p);
                    var img = _images[i * 10 + j];
                    img.Draw(new RectangleF(p, new SizeF(_size, _size)));
                }
            }
            base.Draw(rect);
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

