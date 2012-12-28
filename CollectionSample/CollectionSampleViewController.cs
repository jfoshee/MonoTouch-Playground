using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace CollectionSample
{
    public partial class CollectionSampleViewController : UIViewController
    {
        public static NSString ViewCellId = new NSString ("ViewCell");

        public CollectionSampleViewController() : base ("CollectionSampleViewController", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            CollectionView.RegisterClassForCell (typeof(ViewCell), ViewCellId);
            CollectionView.DataSource = new DataSource();
//            CollectionView.CollectionViewLayout = new UICollectionViewLayout();
//            CollectionView.RegisterClassForSupplementaryView (typeof(Header), UICollectionElementKindSection.Header, headerId);
        }
    }
}

