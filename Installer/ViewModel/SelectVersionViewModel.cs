using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Installer.Gateway;
using Installer.Model;

namespace Installer.ViewModel;

public partial class SelectVersionViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<ApplicationVersion> _applicationVersions = [];

    [ObservableProperty] private ApplicationVersion _selectedVersion;
    [ObservableProperty] private Application _selectedApplication;
    
    private readonly IApplicationModel _applicationModel;
    
    public SelectVersionViewModel()
    {
        _applicationModel = new ApplicationGateway();
        SelectedApplication = MainWindowViewModel.Instance.SelectedApplication;
        _ = GetApplicationVersions();
    }

    private async Task GetApplicationVersions()
    {
        var appId = SelectedApplication.ApplicationId;
        ApplicationVersions = new ObservableCollection<ApplicationVersion>(await _applicationModel.GetApplicationVersions(appId));
        SelectedVersion = ApplicationVersions.First();
    }
    
    [RelayCommand]
    private void Install()
    {
        MainWindowViewModel.Instance.ApplicationVersion = SelectedVersion;
        MainWindowViewModel.Instance.Navigate("Installation");
    }
}