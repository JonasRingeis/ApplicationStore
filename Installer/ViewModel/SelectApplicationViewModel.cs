using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Installer.Gateway;
using Installer.Model;
using ListView = Wpf.Ui.Controls.ListView;

namespace Installer.ViewModel;

public partial class SelectApplicationViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Application> _applications = [];

    [ObservableProperty] private bool _installButtonEnabled;
    [ObservableProperty] private int _selectedApplication;
    
    private readonly IApplicationModel _applicationModel;
    
    public SelectApplicationViewModel()
    {
        InstallButtonEnabled = false;
        _applicationModel = new ApplicationGateway();
        _ = GetAllApplications();
    }

    private async Task GetAllApplications()
    {
        Applications = new ObservableCollection<Application>(await _applicationModel.GetAllApplications());
    }
    
    public void ApplicationClick(object sender, bool isDoubleClick)
    {
        InstallButtonEnabled = true;

        var selectedItem = (sender as ListView)!.SelectedItem as Application;
        SelectedApplication = selectedItem!.ApplicationId;
        
        if (!isDoubleClick) return;
        
        MainWindowViewModel.Instance.ApplicationId = SelectedApplication;
        MainWindowViewModel.Instance.Navigate("SelectVersion");
    }

    [RelayCommand]
    private void Install()
    {
        MainWindowViewModel.Instance.ApplicationId = SelectedApplication;
        MainWindowViewModel.Instance.Navigate("SelectVersion");
    }
}