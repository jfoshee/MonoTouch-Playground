// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace PhotoPicker
{
	[Register ("PhotoPickerViewController")]
	partial class PhotoPickerViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton ChooseButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView ImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ChooseButton != null) {
				ChooseButton.Dispose ();
				ChooseButton = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}
		}
	}
}
