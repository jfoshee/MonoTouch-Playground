using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CollectionSample
{
    public class ViewCell : UICollectionViewCell
    {
        [Export ("initWithFrame:")]
        public ViewCell (System.Drawing.RectangleF frame) : base (frame)
        {
            var label = new UILabel(frame);
            label.Text = "HELOO!";
            ContentView.AddSubview(label);
            BackgroundView = new UIView{BackgroundColor = UIColor.Orange};
        }
    }
}

