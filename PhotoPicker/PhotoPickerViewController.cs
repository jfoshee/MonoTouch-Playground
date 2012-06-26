using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace PhotoPicker
{
    public partial class PhotoPickerViewController : UIViewController
    {
        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public PhotoPickerViewController()
			: base (UserInterfaceIdiomIsPhone ? "PhotoPickerViewController_iPhone" : "PhotoPickerViewController_iPad", null)
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
			
            // Perform any additional setup after loading the view, typically from a nib.
            //ChooseButton.TouchUpInside += (sender, e) => PhotoSelection();
            ChooseButton.TouchUpInside += AttachImageBtnTouched;
        }
		
        UIImagePickerController _imagePicker;
        UIPopoverController _popover;

        void AttachImageBtnTouched(object sender, EventArgs e)
        {
            if (_imagePicker == null)
                _imagePicker = new UIImagePickerController();
            if (_popover == null && !UserInterfaceIdiomIsPhone)
                _popover = new UIPopoverController(_imagePicker);
    
            if (_imagePicker.Delegate == null)
            {
                ImagePickerDelegate imgDel = new ImagePickerDelegate(ImageView, _popover);
                _imagePicker.Delegate = imgDel;
                _imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
            }

            if (UserInterfaceIdiomIsPhone)
            {
                UIActionSheet actionSheet = new UIActionSheet("") { "Existing", "Snap new", "Cancel" };
                actionSheet.Style = UIActionSheetStyle.BlackTranslucent;
                actionSheet.ShowInView(View);
                actionSheet.Clicked += delegate(object s, UIButtonEventArgs action) {
                    switch(action.ButtonIndex)
                    {
                        case 0:
                            _imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
                            //_imagePicker.AllowsEditing = true;
                            this.PresentModalViewController(_imagePicker, true);
                            break;
                        case 1:
                            _imagePicker.SourceType = UIImagePickerControllerSourceType.Camera;
                            //_imagePicker.AllowsEditing = true;
                            this.PresentModalViewController(_imagePicker, true);
                            break;
                        default:
                            break;
                    }
               };
            }
            else
            {
                if (_popover.PopoverVisible)
                {
                    _popover.Dismiss(true);
                    _imagePicker.Dispose();
                    _popover.Dispose();
                    return;
                }
                else
                {
                    _popover.PresentFromRect((sender as UIView).Frame, this.View, UIPopoverArrowDirection.Any, true);
                }
            }
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
            return true;
        }
    }

    public class ImagePickerDelegate : UIImagePickerControllerDelegate
    {
        UIImageView _imageView;
        UIPopoverController _popover;

        public ImagePickerDelegate(UIImageView imageView, UIPopoverController popover)
        {
            _imageView = imageView;
            _popover = popover;
        }

        public override void FinishedPickingMedia(UIImagePickerController picker, NSDictionary info)
        {
            UIImage image = (UIImage)info.ObjectForKey(new NSString("UIImagePickerControllerOriginalImage"));
            // do whatever else you'd like to with the image
            Console.WriteLine("media {0} x {1}", image.CGImage.Width, image.CGImage.Height);
            picker.DismissModalViewControllerAnimated(true);
            _imageView.Image = image;
            if (_popover != null && _popover.PopoverVisible)
                _popover.Dismiss(true);
        }
//
//        public override void FinishedPickingImage(UIImagePickerController picker, UIImage image, NSDictionary editingInfo)
//        {
//            Console.WriteLine("image {0} x {1}", image.CGImage.Width, image.CGImage.Height);
//            //picker.DismissModalViewControllerAnimated(true);
//        }
    }
}

