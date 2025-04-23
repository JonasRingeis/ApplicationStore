using CommunityToolkit.Mvvm.ComponentModel;

namespace Installer.ViewModel;

public partial class InstallationViewModel : ObservableObject
{
    [ObservableProperty]
    private string? _downloadVisibility, _installationVisibility;
    
    [ObservableProperty]
    private float? _downloadProgress;
    
    public InstallationViewModel()
    {
        MockInstallation();
    }

    private async Task MockInstallation()
    {
        DownloadVisibility = "Visible";
        InstallationVisibility = "Collapsed";
        DownloadProgress = 0;

        await Task.Delay(2000);
        DownloadProgress = 40;
        await Task.Delay(3000);

        DownloadProgress = 100;
        DownloadVisibility = "Collapsed";
        InstallationVisibility = "Visible";
    }
}