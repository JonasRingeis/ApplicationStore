using System.Windows.Controls;
using System.Windows.Input;
using Installer.ViewModel;

namespace Installer.View.Pages;

public partial class SelectApplication : UserControl
{
    private readonly SelectApplicationViewModel _viewModel;

    public SelectApplication()
    {
        InitializeComponent();
        
        _viewModel = App.GetRequiredService<SelectApplicationViewModel>();
        DataContext = _viewModel;

        _ = _viewModel.GetAllApplications();
    }
    
    private void HandleApplicationClick(object sender, MouseButtonEventArgs _)
    {
        _viewModel.ApplicationClick(sender, false);
    }
    private void HandleApplicationDoubleClick(object sender, MouseButtonEventArgs _)
    {
        _viewModel.ApplicationClick(sender, true);
    }
}