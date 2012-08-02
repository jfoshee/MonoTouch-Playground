using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace NoXib
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow _window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);
            _window.RootViewController = new AnotherController();
            _window.MakeKeyAndVisible();
            return true;
        }
    }
}

