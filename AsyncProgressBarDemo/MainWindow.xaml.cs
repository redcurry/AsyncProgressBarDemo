using System;
using System.Threading;
using System.Windows;

namespace AsyncProgressBarDemo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DoWork_OnClick(object sender, RoutedEventArgs e)
        {
            ProgressDialogForWorkOnCurrentThread.Start(Work);
        }

        private void Work()
        {
            Console.WriteLine($"Starting work from thread {Thread.CurrentThread.ManagedThreadId}...");
            Thread.Sleep(5000);
            Console.WriteLine("Done with work.");
        }
    }
}
