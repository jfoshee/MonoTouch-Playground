using MonoTouch.UIKit;

namespace NoXib
{
    public class SimpleView : UIView
    {
        public UILabel TitleLabel { get; private set; }

        public SimpleView()
        {
            BackgroundColor = UIColor.Green;
            TitleLabel = new UILabel { AutoresizingMask = UIViewAutoresizing.FlexibleMargins };
            AddSubview(TitleLabel);
        }
    }
}
