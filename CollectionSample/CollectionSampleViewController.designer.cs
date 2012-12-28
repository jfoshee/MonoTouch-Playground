// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace CollectionSample
{
	[Register ("CollectionSampleViewController")]
	partial class CollectionSampleViewController
	{
		[Outlet]
		MonoTouch.UIKit.UICollectionView CollectionView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (CollectionView != null) {
				CollectionView.Dispose ();
				CollectionView = null;
			}
		}
	}
}
