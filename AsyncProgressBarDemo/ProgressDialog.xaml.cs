using System;
using System.Threading;
using System.Windows;

namespace AsyncProgressBarDemo
{
    public partial class ProgressDialog : Window
    {
        private readonly Action work;
        private readonly SynchronizationContext workSyncContext;

        public ProgressDialog(Action work, SynchronizationContext workSyncContext)
        {
            this.work = work;
            this.workSyncContext = workSyncContext;

            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Capture this dialog's SynchronizationContext for closing the dialog later
            var dialogSyncContext = SynchronizationContext.Current;

            // Perform the work on the given SynchronizationContext (not the dialog's)
            workSyncContext.Post(x =>
            {
                work?.Invoke();

                // Close the dialog once the work has been completed
                // (this action must be done on the dialog's SynchronizationContext)
                dialogSyncContext.Post(_ => Close(), null);
            }, null);
        }
    }
}
