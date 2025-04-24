using System.Windows.Controls;
using System.Windows.Input;
using Installer.ViewModel;

namespace Installer.View.Pages;

public partial class SelectApplication : UserControl
{
    public SelectApplication()
    {
        InitializeComponent();
        DataContext = new SelectApplicationViewModel();
    }

    private void HandleApplicationClick(object sender, MouseButtonEventArgs e)
    {
        var installImmediate = e.ClickCount >= 2; 
        ((SelectApplicationViewModel)DataContext).ApplicationClick(sender, installImmediate);
    }
}