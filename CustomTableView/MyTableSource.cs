using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CustomTableView
{
    public class MyTableSource : UITableViewSource
    {
        TableCellFactory<MyCustomCell> factory = new TableCellFactory<MyCustomCell>("CellID", "MyCustomCell");
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = factory.GetCell(tableView);
            cell.BindDataToCell("some data");
            
            return cell;
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return 10;
        }
    }
}

