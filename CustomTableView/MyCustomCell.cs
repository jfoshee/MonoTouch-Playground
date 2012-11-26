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
    }
}

