using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using MonoTouch.ObjCRuntime;

namespace CustomTableView
{
    public class TableCellFactory<T> where T : UITableViewCell
    {
        string _cellId;
        string _nibName;
        
        public TableCellFactory(string cellId, string nibName)
        {
            _cellId = cellId;
            _nibName = nibName;
        }
        
        public T GetCell(UITableView tableView)
        {
            var cell = tableView.DequeueReusableCell(_cellId) as T;
            
            if (cell == null)
            {
                cell = Activator.CreateInstance<T>();
                var views = NSBundle.MainBundle.LoadNib(_nibName, cell, null);
                cell = Runtime.GetNSObject( views.ValueAt(0) ) as T;
            }
            
            return cell;
        }
    }
}

