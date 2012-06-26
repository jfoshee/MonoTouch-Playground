using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreImage;

namespace FilterToy
{
    class FilterPickerViewModel : UIPickerViewModel
    {
        string[] _filterNames;

        public Action<string> OnFilterSelected { get; set; }

        public FilterPickerViewModel(Action<string> onFilterSelected)
        {
            _filterNames = CIFilter.FilterNamesInCategories(new string [] { });
            OnFilterSelected = onFilterSelected;
            OnFilterSelected(_filterNames [0]);
        }

        public override int GetComponentCount(UIPickerView picker)
        {
            return 1;
        }

        public override int GetRowsInComponent(UIPickerView uipv, int comp)
        {
            return _filterNames.Length;
        }
        
        public override string GetTitle(UIPickerView uipv, int row, int comp)
        {
            return _filterNames [row];
        }

        public override void Selected(UIPickerView uipv, int row, int comp)
        {
            OnFilterSelected(_filterNames [row]);
//            Console.WriteLine(_filterNames[row]);
        }
    }

    public partial class FilterToyViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            FilterPicker.Model = new FilterPickerViewModel(OnFilterSelected);
            var uiImage = new UIImage("Images/photo.jpg");
            ImageView.Image = uiImage;
        }

        static void SetImage(CIFilter filter, string name)
        {
            if (filter.InputKeys.Contains(name))
            {
                var uiImage = new UIImage("Images/" + name + ".jpg");
                var inputImage = CIImage.FromCGImage(uiImage.CGImage);
                filter[new NSString(name)] = inputImage;
            }
        }

        static void DisplayFilterOutput(CIFilter filter, UIImageView imageView)
        {
            CIImage output = filter.OutputImage;
            if (output == null)
                return;
            var context = CIContext.FromOptions(null);
            var renderedImage = context.CreateCGImage(output, output.Extent);
            var finalImage = new UIImage(renderedImage);
            imageView.Image = finalImage;
        }

        static void PrintFilterInfo(CIFilter filter)
        {
            Console.WriteLine(filter.Name);
            foreach (var key in filter.InputKeys)
            {
                var attributes = (NSDictionary)filter.Attributes[new NSString(key)];
                var attributeClass = attributes[new NSString("CIAttributeClass")];
                Console.WriteLine("  {0} : {1}", key, attributeClass);
            }
        }

        static void HandleColorCubeFilter(CIFilter filter)
        {
            if (filter.Name != "CIColorCube")
                return;

            int dimension = 64;  // Must be power of 2, max of 128 (max of 64 on ios)
            int cubeDataSize = 4 * dimension * dimension * dimension;
            filter[new NSString("inputCubeDimension")] = new NSNumber(dimension);

            // 2 : 32 /4 = 8 = 2^3
            // 4 : 256 /4 = 64 = 4^3
            // 8 : 2048 /4 = 512 = 8^3

            var cubeData = new byte[cubeDataSize];
            var rnd = new Random();
            rnd.NextBytes(cubeData);
            for (int i = 3; i < cubeDataSize; i += 4)
                cubeData[i] = 255;
            filter[new NSString("inputCubeData")] = NSData.FromArray(cubeData);
        }

        void OnFilterSelected(string filterName)
        {
            var filter = CIFilter.FromName(filterName);
            PrintFilterInfo(filter);
            SetImage(filter, "inputImage");
            SetImage(filter, "inputBackgroundImage");
            HandleColorCubeFilter(filter);
            DisplayFilterOutput(filter, ImageView);
        }

        #region BoilerPlate

        static bool UserInterfaceIdiomIsPhone
        {
            get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
        }

        public FilterToyViewController()
            : base (UserInterfaceIdiomIsPhone ? "FilterToyViewController_iPhone" : "FilterToyViewController_iPad", null)
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

        #endregion
    }
}

