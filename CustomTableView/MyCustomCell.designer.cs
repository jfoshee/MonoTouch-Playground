// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace CustomTableView
{
	partial class MyCustomCell
	{
		[Outlet]
		MonoTouch.UIKit.UILabel LabelA { get; set; }

		[Outlet]
		MonoTouch.UIKit.UILabel LabelB { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (LabelA != null) {
				LabelA.Dispose ();
				LabelA = null;
			}

			if (LabelB != null) {
				LabelB.Dispose ();
				LabelB = null;
			}
		}
	}
}
