using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using Installer.Gateway;
using Installer.Model;

namespace Installer.ViewModel;

public partial class SelectApplicationViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Application> _applications = [];
    
    private readonly ApplicationGateway _applicationGateway;
    
    public SelectApplicationViewModel()
    {
        _applicationGateway = new ApplicationGateway();
        _ = GetAllApplications();
    }

    private async Task GetAllApplications()
    {
        Applications = new ObservableCollection<Application>(await _applicationGateway.GetAllApplications());
    }
}