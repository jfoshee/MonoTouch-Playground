using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Appearance
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;
        AppearanceViewController viewController;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            InitializeStyle();
            window = new UIWindow(UIScreen.MainScreen.Bounds);
            viewController = new AppearanceViewController();
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();
            return true;
        }

        static void InitializeStyle()
        {
            UIView.Appearance.BackgroundColor = UIColor.Blue;
        }
    }
}
