using System.IO;
using System.Net;
using System.Net.Http;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Installer.Gateway;
using Installer.Model;
using Installer.View;
using Installer.ViewModel.Installation;

namespace Installer.ViewModel;

public partial class InstallationViewModel : ObservableObject
{
    [ObservableProperty]
    private Visibility? _downloadVisibility, _checksumVisibility, _installationVisibility = Visibility.Collapsed;
    
    [ObservableProperty]
    private float? _downloadProgress, _checksumProgress;

    [ObservableProperty] private InstallationData? _installationData;
    
    private readonly IApplicationModel _applicationModel;
    private readonly Downloader _downloader;

    public InstallationViewModel()
    {
        _applicationModel = new ApplicationGateway();
        _downloader = new Downloader();
        _ = PerformInstallation();
    }

    private async Task PerformInstallation()
    {
        var versionId = MainWindowViewModel.Instance.ApplicationVersion.ApplicationVersionId;
        InstallationData = await _applicationModel.GetInstallationData(versionId);

        await Download();

        await VerifyChecksum();

        await MockInstallation();
    }

    private async Task Download()
    {
        DownloadVisibility = Visibility.Visible;
        ChecksumVisibility = Visibility.Collapsed;
        InstallationVisibility = Visibility.Collapsed;
        
        var progress = new Progress<float>(p =>
        {
            DownloadProgress = p * 100f;
        });
        var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        dir = Path.Combine(dir, "Premium-Installer");
        Directory.CreateDirectory(dir);

        var fileName = $"{MainWindowViewModel.Instance.SelectedApplication.ApplicationId}-{MainWindowViewModel.Instance.ApplicationVersion.VersionName}.raw";
        var path = Path.Combine(dir, fileName);
        
        Console.WriteLine($"Saving to file: {path}");
        await _downloader.DownloadPackages(InstallationData!.DownloadUrl, path, progress);
    }

    private async Task VerifyChecksum()
    {
        DownloadVisibility = Visibility.Collapsed;
        ChecksumVisibility = Visibility.Visible;
        for (var i = 0; i < 100; i++)
        {
            ChecksumProgress = i;
            await Task.Delay((int)Math.Round(30 - 0.0025f * Math.Pow(i, 2)));
        }
        ChecksumProgress = 100;
    }
    
    private async Task MockInstallation()
    {
        ChecksumVisibility = Visibility.Collapsed;
        DownloadVisibility = Visibility.Collapsed;
        InstallationVisibility = Visibility.Visible;
    }
}