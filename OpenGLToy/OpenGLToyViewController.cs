using MonoTouch.UIKit;

namespace OpenGLToy
{
    public partial class OpenGLToyViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public OpenGLToyViewController()
			: base (UserInterfaceIdiomIsPhone ? "OpenGLToyViewController_iPhone" : "OpenGLToyViewController_iPad", null)
        {
        }
		
        public override void DidReceiveMemoryWarning()
        {
            // Releases the view if it doesn't have a superview.
            base.DidReceiveMemoryWarning();
			
            // Release any cached data, images, etc that aren't in use.
        }
		
        SceneRenderer _sceneRenderer;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            // Perform any additional setup after loading the view, typically from a nib.

            SceneView.StartAnimating();
            _sceneRenderer = new SceneRenderer();
            SceneView.DoRender += _sceneRenderer.Render;
            // TODO: Start/Stop animating as we resign or become active
        }
		
        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
			
            // Clear any references to subviews of the main view in order to
            // allow the Garbage Collector to collect them sooner.
            //
            // e.g. myOutlet.Dispose (); myOutlet = null;
			
            ReleaseDesignerOutlets();

            _sceneRenderer.Dispose();
            _sceneRenderer = null;
            SceneView.DoRender -= _sceneRenderer.Render;
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

