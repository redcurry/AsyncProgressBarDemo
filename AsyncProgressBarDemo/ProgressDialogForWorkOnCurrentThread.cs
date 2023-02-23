using System;
using System.Threading;

namespace AsyncProgressBarDemo
{
    public class ProgressDialogForWorkOnCurrentThread
    {
        public static void Start(Action work)
        {
            // Capture the current SynchronizationContext, which is linked to
            // the UI thread (if this method is called from the UI thread)
            var syncContext = SynchronizationContext.Current;

            // Create a new thread on which to show the progress dialog
            var thread = new Thread(() =>
            {
                var progressDialog = new ProgressDialog(work, syncContext);
                progressDialog.ShowDialog();

                // Start a new Dispatcher to keep this thread alive while showing the dialog
                // (it's not strictly needed when calling ShowDialog() instead of Show())
                System.Windows.Threading.Dispatcher.Run();
            });

            // Required by WPF controls
            thread.SetApartmentState(ApartmentState.STA);

            // Automatically kill this thread when the application ends
            thread.IsBackground = true;

            thread.Start();
        }
    }
}