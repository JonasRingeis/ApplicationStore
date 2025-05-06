using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Installer.Gateway;
using Installer.Model;
using Installer.View.UserControls;
using Wpf.Ui;

namespace Installer.ViewModel;

public partial class SelectVersionViewModel(
    IContentDialogService dialogService,
    IApplicationModel applicationModel) : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ApplicationVersion> _applicationVersions = [];

    [ObservableProperty] private ApplicationVersion _selectedVersion;
    [ObservableProperty] private Application _selectedApplication;

    public async Task GetApplicationVersions()
    {
        SelectedApplication = MainWindowViewModel.Instance.SelectedApplication;
        var appId = SelectedApplication.ApplicationId;
        ApplicationVersions = new ObservableCollection<ApplicationVersion>(await applicationModel.GetApplicationVersions(appId));
        SelectedVersion = ApplicationVersions.First();
    }
    
    [RelayCommand]
    private void Install()
    {
        MainWindowViewModel.Instance.ApplicationVersion = SelectedVersion;
        MainWindowViewModel.Instance.Navigate("Installation");
    }

    [RelayCommand]
    private async Task OnShowEulaDialog()
    {
        var dialog = new EulaDialog(dialogService.GetDialogHost());
        await dialog.ShowAsync();
    }
}