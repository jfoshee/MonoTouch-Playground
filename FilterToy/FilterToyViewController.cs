using System;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreImage;
using MonoTouch.CoreGraphics;

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
        }
    }

    public partial class FilterToyViewController : UIViewController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            FilterPicker.Model = new FilterPickerViewModel(OnFilterSelected);
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

        static void HandleFalseColorFilter(CIFilter filter)
        {
            if (filter.Name != "CIFalseColor")
                return;
            var inputColor0 = new CIColor(UIColor.Red);
            var inputColor1 = new CIColor(UIColor.Blue);
            filter[new NSString("inputColor0")] = inputColor0;
            filter[new NSString("inputColor1")] = inputColor1;
        }

        void HandleTemperatureTintFilter(CIFilter filter)
        {
            if (filter.Name != "CITemperatureAndTint")
                return;
            filter[new NSString("inputNeutral")] = new CIVector(6500, 700);
            filter[new NSString("inputTargetNeutral")] = new CIVector(6500, 200);
//inputNeutral
//A CIVector object whose attribute type is CIAttributeTypeOffset and whose display name is Neutral.
//Default value: [6500, 0]
//inputTargetNeutral
//A CIVector object whose attribute type is CIAttributeTypeOffset and whose display name is TargetNeutral
//Default value: [6500, 0]
        }

        void MonochromeExample()
        {
            var uiImage = new UIImage("Images/photo.jpg");
            CGImage image = uiImage.CGImage;
            var mono = new CIColorMonochrome
            {
                Color = CIColor.FromRgb(1, 1, 1),
                Intensity = 1.0f,
                Image = CIImage.FromCGImage(image)
            };
            DisplayFilterOutput(mono, ImageView);
        }

        void OnFilterSelected(string filterName)
        {
            var filter = CIFilter.FromName(filterName);
            PrintFilterInfo(filter);
            SetImage(filter, "inputImage");
            SetImage(filter, "inputBackgroundImage");
            HandleColorCubeFilter(filter);
            HandleFalseColorFilter(filter);
            HandleTemperatureTintFilter(filter);
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

