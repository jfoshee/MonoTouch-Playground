using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.GLKit;
using OpenTK.Graphics.ES20;
using MonoTouch.OpenGLES;

namespace GLK1
{
    class GraphicsDelegate : GLKViewDelegate
    {
        public override void DrawInRect(GLKView view, RectangleF rect)
        {
            Console.WriteLine(rect);
            GL.ClearColor(Color.MistyRose);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }

    public partial class GLK1ViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public GLK1ViewController()
			: base (UserInterfaceIdiomIsPhone ? "GLK1ViewController_iPhone" : "GLK1ViewController_iPad", null)
        {
        }
		
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
//            GraphicsView.AutosizesSubviews = true;
//            GraphicsView.AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
            EAGLContext context = new EAGLContext(EAGLRenderingAPI.OpenGLES2);
//            GraphicsView.Context = context;
            // Perform any additional setup after loading the view, typically from a nib.
            GraphicsView.Delegate = new GraphicsDelegate();
            GraphicsView.EnableSetNeedsDisplay = true;
//            GraphicsView.SetNeedsDisplay();
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();
            EAGLContext context = new EAGLContext(EAGLRenderingAPI.OpenGLES2);
            GraphicsView.Context = context;
            GraphicsView.SetNeedsDisplay();
        }
		
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
			
            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;
			
            ReleaseDesignerOutlets();
        }
		
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            // Return true for supported orientations
            if (UserInterfaceIdiomIsPhone)
            {
                return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
            } else
            {
                return true;
            }
        }
    }
}

