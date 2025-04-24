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

        for (int i = 0; i < 100; i++)
        {
            DownloadProgress = i;
            await Task.Delay((int)Math.Round(50 - 0.005f * Math.Pow(i, 2)));
        }

        DownloadProgress = 100;
        DownloadVisibility = "Collapsed";
        InstallationVisibility = "Visible";
    }
}