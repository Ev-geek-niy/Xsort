using System.Drawing;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using Xsort.Services.Services.Interfaces;

namespace Xsort.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly IRegistryService _registryService;
    
    public MainWindow()
    {
        InitializeComponent();
        Hide();
    }

    private void HandleOpen_OnClick(object sender, RoutedEventArgs e)
    {
        
    }

    private void HandleClose_OnClick(object sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}