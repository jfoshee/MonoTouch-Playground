using System.Drawing;
using MonoTouch.UIKit;

namespace Appearance
{
    public partial class AppearanceViewController : UIViewController
    {
        public AppearanceViewController() : base ("AppearanceViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var frame = View.Bounds;
            frame.Height /= 3;
            frame.Y = 2 * frame.Height;
            var container = new MyAppearanceContainer(frame);
            container.BackgroundColor = UIColor.Blue;
            View.AddSubview(container);
            var button = new UIButton(UIButtonType.RoundedRect) { 
                Frame = RectangleF.FromLTRB(10, 10, 100, 40)
            };
            button.SetTitle("Contained", UIControlState.Normal);
            container.AddSubview(button);
            UIButton.AppearanceWhenContainedIn(typeof(MyAppearanceContainer)).SetTitleColor(
                UIColor.Red, UIControlState.Normal);
        }
    }
}
