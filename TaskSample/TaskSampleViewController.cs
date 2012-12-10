using System;
using System.Threading.Tasks;
using System.Threading;
using MonoTouch.UIKit;

namespace TaskSample
{
    class TaskManager
    {
        CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public Task<string> GenerateText()
        {
            return Task<string>.Factory.StartNew(() => {
                for (int i = 0; i < 70; i++) {
                    Thread.Sleep(100);
                    _cancellationTokenSource.Token.ThrowIfCancellationRequested();
                }
                return "Finished";
            }, _cancellationTokenSource.Token);
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
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
            Label.Text = "Waiting...";
            ActivityIndicator.StartAnimating();
            var taskMan = new TaskManager();
            var task = taskMan.GenerateText();
            task.ContinueWith((t) => InvokeOnMainThread(() => {
                ActivityIndicator.StopAnimating();
                Label.Text = t.IsCanceled ? "Canceled" : t.Result;
            }));
            CancelButton.TouchUpInside += (sender, e) => taskMan.Cancel();
        }

        // ability to spawn new task
    }
}
