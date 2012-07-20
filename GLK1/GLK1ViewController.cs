using System;
using System.Drawing;
using MonoTouch.UIKit;
using MonoTouch.GLKit;
using MonoTouch.OpenGLES;
using OpenTK.Graphics.ES20;

namespace GLK1
{
    class GraphicsDelegate : GLKViewDelegate
    {
        public override void DrawInRect(GLKView view, RectangleF rect)
        {
            Console.WriteLine(rect);
            GL.ClearColor(Color.Maroon);
            GL.Clear(ClearBufferMask.ColorBufferBit);
        }
    }

    public partial class GLK1ViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            GraphicsView.Delegate = new GraphicsDelegate();
            GraphicsView.EnableSetNeedsDisplay = true;
        }

        public override void ViewWillLayoutSubviews()
        {
            base.ViewWillLayoutSubviews();
            EAGLContext context = new EAGLContext(EAGLRenderingAPI.OpenGLES2);
            GraphicsView.Context = context;
            GraphicsView.SetNeedsDisplay();
        }

        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public GLK1ViewController()
			: base (UserInterfaceIdiomIsPhone ? "GLK1ViewController_iPhone" : "GLK1ViewController_iPad", null)
        {
        }
		
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
            ReleaseDesignerOutlets();
        }
		
        public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
        {
            return true;
        }
    }
}

