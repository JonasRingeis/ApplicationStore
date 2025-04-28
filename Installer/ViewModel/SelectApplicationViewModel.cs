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

    [ObservableProperty] private bool _selectButtonEnabled;
    [ObservableProperty] private Application _selectedApplication;
    
    private readonly IApplicationModel _applicationModel;
    
    public SelectApplicationViewModel()
    {
        SelectButtonEnabled = false;
        _applicationModel = new ApplicationGateway();
        _ = GetAllApplications();
    }

    private async Task GetAllApplications()
    {
        Applications = new ObservableCollection<Application>(await _applicationModel.GetAllApplications());
    }
    
    public void ApplicationClick(object sender, bool isDoubleClick)
    {
        SelectButtonEnabled = true;

        SelectedApplication = ((sender as ListView)!.SelectedItem as Application)!;
        
        if (!isDoubleClick) return;
        
        MainWindowViewModel.Instance.SelectedApplication = SelectedApplication;
        MainWindowViewModel.Instance.Navigate("SelectVersion");
    }

    [RelayCommand]
    private void SelectApp()
    {
        MainWindowViewModel.Instance.SelectedApplication = SelectedApplication;
        MainWindowViewModel.Instance.Navigate("SelectVersion");
    }
}