using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CustomTableView
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        UIWindow window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);
            var viewController = new UITableViewController();
            viewController.TableView.Source = new MyTableSource();
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();
            return true;
        }
    }
}

