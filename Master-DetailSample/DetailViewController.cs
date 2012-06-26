using System;
using System.Drawing;
using System.Collections.Generic;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace MasterDetailSample
{
	public partial class DetailViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		UIPopoverController popoverController;
		object detailItem;
		
		public DetailViewController ()
			: base (UserInterfaceIdiomIsPhone ? "DetailViewController_iPhone" : "DetailViewController_iPad", null)
		{
			this.Title = NSBundle.MainBundle.LocalizedString ("Detail", "Detail");
		}
		
		public void SetDetailItem (object newDetailItem)
		{
			if (detailItem != newDetailItem) {
				detailItem = newDetailItem;
				
				// Update the view
				ConfigureView ();
			}
			
			if (this.popoverController != null)
				this.popoverController.Dismiss (true);
		}
		
		void ConfigureView ()
		{
			// Update the user interface for the detail item
			if (detailItem != null)
				this.detailDescriptionLabel.Text = detailItem.ToString ();
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			if (UserInterfaceIdiomIsPhone) {
				return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
			} else {
				return true;
			}
		}
		
		[Export("splitViewController:willHideViewController:withBarButtonItem:forPopoverController:")]
		public void WillHideViewController (UISplitViewController svc, UIViewController vc,
			UIBarButtonItem barButtonItem, UIPopoverController pc)
		{
			barButtonItem.Title = "Master";
			var items = new List<UIBarButtonItem> ();
			items.Add (barButtonItem);
			items.AddRange (toolbar.Items);
			toolbar.SetItems (items.ToArray (), true);
			popoverController = pc;
		}
		
		[Export("splitViewController:willShowViewController:invalidatingBarButtonItem:")]
		public void WillShowViewController (UISplitViewController svc, UIViewController vc,
			UIBarButtonItem button)
		{
			// Called when the view is shown again in the split view, invalidating the button and popover controller.
			var items = new List<UIBarButtonItem> (toolbar.Items);
			items.RemoveAt (0);
			toolbar.SetItems (items.ToArray (), true);
			popoverController = null;
		}
	}
}

