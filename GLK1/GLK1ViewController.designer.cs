// WARNING
//
// This file has been generated automatically by MonoDevelop to store outlets and
// actions made in the Xcode designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;

namespace GLK1
{
	[Register ("GLK1ViewController")]
	partial class GLK1ViewController
	{
		[Outlet]
		MonoTouch.GLKit.GLKView GraphicsView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (GraphicsView != null) {
				GraphicsView.Dispose ();
				GraphicsView = null;
			}
		}
	}
}
