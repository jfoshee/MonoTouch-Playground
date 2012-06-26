// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace FilterToy
{
	[Register ("FilterToyViewController")]
	partial class FilterToyViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIPickerView FilterPicker { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView ImageView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FilterPicker != null) {
				FilterPicker.Dispose ();
				FilterPicker = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}
		}
	}
}
