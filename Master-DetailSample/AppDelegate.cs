using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MasterDetailSample
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UIWindow window;
		UINavigationController navigationController;
		UISplitViewController splitViewController;
		
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			// load the appropriate UI, depending on whether the app is running on an iPhone or iPad
			if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone) {
				var controller = new RootViewController ();
				navigationController = new UINavigationController (controller);
				window.RootViewController = navigationController;
			} else {
				var controller = new RootViewController ();
				var navigationController = new UINavigationController (controller);
				var detailViewController = new DetailViewController ();
				splitViewController = new UISplitViewController ();
				splitViewController.WeakDelegate = detailViewController;
				splitViewController.ViewControllers = new UIViewController[] {
					navigationController,
					detailViewController
				};
				window.RootViewController = splitViewController;
			}

			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

