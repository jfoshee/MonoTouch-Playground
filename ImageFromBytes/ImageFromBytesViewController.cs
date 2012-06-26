using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;

namespace ImageFromBytes
{
	public partial class ImageFromBytesViewController : UIViewController
	{
		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public ImageFromBytesViewController ()
			: base (UserInterfaceIdiomIsPhone ? "ImageFromBytesViewController_iPhone" : "ImageFromBytesViewController_iPad", null)
		{
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
			//ImageView.Image = new UIImage(@"/Library/Application Support/Apple/iChat Icons/Flags/Brazil.gif");
			
			var bytes = new byte[] { 
				255, 000, 000, 255,
				255, 000, 000, 255,
				255, 000, 000, 128,
				255, 000, 000, 050,
				
				255, 255, 255, 255,
				255, 255, 255, 255,
				255, 255, 255, 128,
				255, 255, 255, 050,

				000, 000, 255, 255,
				000, 000, 255, 255,
				000, 000, 255, 128,
				000, 000, 255, 050,
			};
			int width = 4;
			
			var uiImage = MakeUIImage (bytes, width);
            //uiImage.SaveToPhotosAlbum(null);
			ImageView.Image = uiImage;
		}

		static UIImage MakeUIImage (byte[] bytes, int width)
		{
			var provider = new CGDataProvider(bytes, 0, bytes.Length);
			int bitsPerComponent = 8;
			int components = 4;
			int height = bytes.Length / components / width;
			int bitsPerPixel = components * bitsPerComponent;
			int bytesPerRow = components * width;	// Tip:  When you create a bitmap graphics context, you’ll get the best performance if you make sure the data and bytesPerRow are 16-byte aligned.
			bool shouldInterpolate = false;
			var colorSpace = CGColorSpace.CreateDeviceRGB();
			var cgImage = new CGImage(width, height, bitsPerComponent, bitsPerPixel, bytesPerRow, 
			                          colorSpace, CGImageAlphaInfo.Last, provider,
			                          null, shouldInterpolate, CGColorRenderingIntent.Default);
			return UIImage.FromImage(cgImage);
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
	}
}

