using System;
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
            var tableData = new string[] 
            {
                "Apple Fruit",
                "Pear Fruit",
                "Carrot Vegetable",
                "Tomato Fruit",
                "Cucumber Not sure",
                "Potato Vegetable",
                "Orange Fruit",
                "Banana Fruit",
            };
            viewController.TableView.Source = new MyTableSource(tableData);
            window.RootViewController = viewController;
            window.MakeKeyAndVisible();
            return true;
        }
    }
}

