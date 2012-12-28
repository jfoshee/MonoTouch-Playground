using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CollectionSample
{
    public class DataSource : UICollectionViewDataSource
    {
        static NSString viewCellId = new NSString ("ViewCell");

        public override int GetItemsCount(UICollectionView collectionView, int section)
        {
            return 12;
        }

        public override UICollectionViewCell GetCell(UICollectionView collectionView, NSIndexPath indexPath)
        {
            var cell = (ViewCell)collectionView.DequeueReusableCell (CollectionSampleViewController.ViewCellId, indexPath);
//            var animal = animals [indexPath.Row];
//            animalCell.Image = animal.Image;
            return cell;
        }
    }
//
//    public class SimpleCollectionViewController : UICollectionViewController
//    {
//        static NSString animalCellId = new NSString ("AnimalCell");
//        static NSString headerId = new NSString ("Header");                                                                         
//        
//        List<IAnimal> animals;
//        
//        public SimpleCollectionViewController (UICollectionViewLayout layout) : base (layout)
//        {
//            animals = new List<IAnimal> ();
//            for (int i = 0; i< 20; i++) {
//                animals.Add (new Monkey ());
//            }
//            
//            //          CollectionView.ContentInset = new UIEdgeInsets (20, 20, 20, 20);
//        }
//        
//        public override void ViewDidLoad ()
//        {
//            base.ViewDidLoad ();
//            
//            CollectionView.RegisterClassForCell (typeof(AnimalCell), animalCellId);
//            CollectionView.RegisterClassForSupplementaryView (typeof(Header), UICollectionElementKindSection.Header, headerId);
//        }
//        
//        public override int NumberOfSections (UICollectionView collectionView)
//        {
//            return 1;
//        }
//        
//        public override int GetItemsCount (UICollectionView collectionView, int section)
//        {
//            return animals.Count;
//        }
//        
//        public override UICollectionViewCell GetCell (UICollectionView collectionView, MonoTouch.Foundation.NSIndexPath indexPath)
//        {
//            var animalCell = (AnimalCell)collectionView.DequeueReusableCell (animalCellId, indexPath);
//            
//            var animal = animals [indexPath.Row];
//            
//            animalCell.Image = animal.Image;
//            
//            return animalCell;
//        }
//        
//        public override UICollectionReusableView GetViewForSupplementaryElement (UICollectionView collectionView, NSString elementKind, NSIndexPath indexPath)
//        {
//            var headerView = (Header)collectionView.DequeueReusableSupplementaryView (elementKind, headerId, indexPath);
//            headerView.Text = "This is a Supplementary View";
//            return headerView;
//        }
//        
//        public override void ItemHighlighted (UICollectionView collectionView, NSIndexPath indexPath)
//        {
//            var cell = collectionView.CellForItem(indexPath);
//            cell.ContentView.BackgroundColor = UIColor.Yellow;
//        }
//        
//        public override void ItemUnhighlighted (UICollectionView collectionView, NSIndexPath indexPath)
//        {
//            var cell = collectionView.CellForItem(indexPath);
//            cell.ContentView.BackgroundColor = UIColor.White;
//        }
//        
//        public override bool ShouldHighlightItem (UICollectionView collectionView, NSIndexPath indexPath)
//        {
//            return true;
//        }
//        
//        //      public override bool ShouldSelectItem (UICollectionView collectionView, NSIndexPath indexPath)
//        //      {
//        //          return false;
//        //      }
//        
//        // for edit menu
//        public override bool ShouldShowMenu (UICollectionView collectionView, NSIndexPath indexPath)
//        {
//            return true;
//        }
//        
//        public override bool CanPerformAction (UICollectionView collectionView, MonoTouch.ObjCRuntime.Selector action, NSIndexPath indexPath, NSObject sender)
//        {
//            return true;
//        }
//        
//        public override void PerformAction (UICollectionView collectionView, MonoTouch.ObjCRuntime.Selector action, NSIndexPath indexPath, NSObject sender)
//        {
//            Console.WriteLine ("code to perform action");
//        }
//    }
//    
//    public class AnimalCell : UICollectionViewCell
//    {
//        UIImageView imageView;
//        
//        [Export ("initWithFrame:")]
//        public AnimalCell (System.Drawing.RectangleF frame) : base (frame)
//        {
//            BackgroundView = new UIView{BackgroundColor = UIColor.Orange};
//            
//            SelectedBackgroundView = new UIView{BackgroundColor = UIColor.Green};
//            
//            ContentView.Layer.BorderColor = UIColor.LightGray.CGColor;
//            ContentView.Layer.BorderWidth = 2.0f;
//            ContentView.BackgroundColor = UIColor.White;
//            ContentView.Transform = CGAffineTransform.MakeScale (0.8f, 0.8f);
//            
//            imageView = new UIImageView (UIImage.FromBundle ("placeholder.png"));
//            imageView.Center = ContentView.Center;
//            imageView.Transform = CGAffineTransform.MakeScale (0.7f, 0.7f);
//            
//            ContentView.AddSubview (imageView);
//        }
//        
//        public UIImage Image {
//            set {
//                imageView.Image = value;
//            }
//        }
//    }
//    
//


}

