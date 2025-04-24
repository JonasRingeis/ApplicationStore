using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Installer.Gateway;
using Installer.Model;

namespace Installer.ViewModel;

public partial class SelectApplicationViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Application> _applications;

    [ObservableProperty] private bool _installButtonEnabled;
    [ObservableProperty] private string _selectedApplication;
    
    private readonly IApplicationModel _applicationModel;
    
    public SelectApplicationViewModel()
    {
        Applications =
        [
            new Application
            {
                Title = "Test App", Description = "Longer test description than title",
                PublisherName = "Some Publisher", ApplicationId = 0, PublisherId = 0
            }
        ];
        InstallButtonEnabled = false;
        _applicationModel = new ApplicationGateway();
        _ = GetAllApplications();
    }

    private async Task GetAllApplications()
    {
        Applications = new ObservableCollection<Application>(await _applicationModel.GetAllApplications());
    }
    
    public void ApplicationClick(object sender, bool installImmediate)
    {
        InstallButtonEnabled = true;
        Console.WriteLine("Install Immediate: " + installImmediate);
        Console.WriteLine(SelectedApplication);
    }
}