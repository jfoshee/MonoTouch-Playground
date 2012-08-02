using MonoTouch.UIKit;

namespace NoXib
{
    public class SimpleController : UIViewController
    {		
        public SimpleController()
        {
            var model = new SimpleModel();
            var simpleView = new SimpleView();
            simpleView.TitleLabel.Text = model.Title;
            simpleView.TitleLabel.SizeToFit();
            View = simpleView;
        }
    }
}
