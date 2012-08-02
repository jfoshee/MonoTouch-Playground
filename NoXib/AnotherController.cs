using MonoTouch.UIKit;

namespace NoXib
{
    public class AnotherController : UIViewController
    {
        public AnotherController()
        {
            View.BackgroundColor = UIColor.Blue;
            var child = new SimpleController();
            AddChildViewController(child);
            View.Add(child.View);
        }
    }
}

