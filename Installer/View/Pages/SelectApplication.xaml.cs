using System.Windows;
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
    
    private SelectApplicationViewModel _viewModel => (SelectApplicationViewModel)DataContext;

    private void HandleApplicationClick(object sender, MouseButtonEventArgs _)
    {
        _viewModel.ApplicationClick(sender, false);
    }
    private void HandleApplicationDoubleClick(object sender, MouseButtonEventArgs _)
    {
        _viewModel.ApplicationClick(sender, true);
    }
}