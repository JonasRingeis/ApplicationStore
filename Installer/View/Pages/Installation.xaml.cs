using System.Windows;
using System.Windows.Controls;
using Installer.ViewModel;
using Wpf.Ui;
using Wpf.Ui.Extensions;

namespace Installer.View.Pages;

public partial class Installation : UserControl
{
    private readonly InstallationViewModel _viewModel;

    public Installation()
    {
        InitializeComponent();
        _viewModel = App.GetRequiredService<InstallationViewModel>();
        DataContext = _viewModel;

        _ = _viewModel.PerformInstallation();
    }

    public void Cancel_Click(object sender, RoutedEventArgs e)
    {
        App.GetRequiredService<IContentDialogService>().ShowSimpleDialogAsync(
            new SimpleContentDialogCreateOptions()
            {
                Title = "Not Implemented",
                Content = "Function not implemented yet.",
                CloseButtonText = "Ok"
            }
        );
    }
}