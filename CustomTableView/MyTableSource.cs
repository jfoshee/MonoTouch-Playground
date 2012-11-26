using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace CustomTableView
{
    public class MyTableSource : UITableViewSource
    {
        string[] _tableItems;
        TableCellFactory<MyCustomCell> _factory = new TableCellFactory<MyCustomCell>("CellID", "MyCustomCell");

        public MyTableSource (string[] items)
        {
            _tableItems = items;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = _factory.GetCell(tableView);
            cell.BindDataToCell(_tableItems[indexPath.Row]);
            return cell;
        }

        public override int RowsInSection(UITableView tableview, int section)
        {
            return _tableItems.Length;
        }
    }
}

