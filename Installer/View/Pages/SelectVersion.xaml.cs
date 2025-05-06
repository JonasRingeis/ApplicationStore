using System.Windows.Controls;
using Installer.ViewModel;

namespace Installer.View.Pages;

public partial class SelectVersion : UserControl
{
    private SelectVersionViewModel _viewModel;
    public SelectVersion()
    {
        InitializeComponent();
        _viewModel = App.GetRequiredService<SelectVersionViewModel>();
        DataContext = _viewModel;

        _ = _viewModel.GetApplicationVersions();
    }
}