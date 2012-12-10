using System;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using System.Threading;

namespace TaskSample
{
    class TaskManager
    {
        public static Task<string> Generate()
        {
            return new Task<string>( () => {
                Thread.Sleep(7000);
                return "Finished";
            });
        }
    }

    public partial class TaskSampleViewController : UIViewController
    {
        public TaskSampleViewController(IntPtr handle) : base (handle)
        {
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            var task = TaskManager.Generate();
            task.Start();
            Label.Text = "Waiting...";
            ActivityIndicator.StartAnimating();
            task.ContinueWith((t) => InvokeOnMainThread(() => {
                ActivityIndicator.StopAnimating();
                Label.Text = t.Result;
            }));
        }

        // ability to cancel task
        // ability to spawn new task
    }
}

