using System.ComponentModel;
using System.Drawing;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
using Xsort.WPF.ViewModels;

namespace Xsort.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : FluentWindow
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        
        Hide();
    }

    private void HandleOpen_OnClick(object sender, RoutedEventArgs e)
    {
        Show();
    }

    private void HandleClose_OnClick(object sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        e.Cancel = true;
        Hide();
    }
}