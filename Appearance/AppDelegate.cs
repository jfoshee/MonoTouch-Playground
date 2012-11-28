using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Appearance
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow _window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            InitializeStyle();
            _window = new UIWindow(UIScreen.MainScreen.Bounds);
            _window.RootViewController = new AppearanceViewController();
            _window.MakeKeyAndVisible();
            return true;
        }

        static void InitializeStyle()
        {
            UIView.Appearance.BackgroundColor = UIColor.Magenta;
        }
    }
}
