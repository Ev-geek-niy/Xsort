using System;
using System.Windows;
using XsortApp.MVVM.ViewModel;
using XsortApp.Services;

namespace XsortApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //TODO: make timer flexible
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(1, 0, 0);
            dispatcherTimer.Start();
            dispatcherTimer_Tick(null, EventArgs.Empty);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            if (DataContext is ApplicationViewModel context)
                ExplorerService.SortFiles(context.AppSettings);
        }
    }
}
