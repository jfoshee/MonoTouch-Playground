using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CustomTableView
{
    [Register("MyCustomCell")]
    public partial class MyCustomCell : UITableViewCell
    {
        public MyCustomCell() : base()
        {
        }
        
        public MyCustomCell(IntPtr handle) : base(handle)
        {
        }

        public void BindDataToCell(string someData)
        {
            var labels = someData.Split(new char[] { ' ' }, 2);
            LabelA.Text = labels[0];
            LabelB.Text = labels[1];
        }
    }
}

