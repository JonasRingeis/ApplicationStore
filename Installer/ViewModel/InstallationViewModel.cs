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
    private Visibility _cancelButtonVisibility = Visibility.Visible;
    
    [ObservableProperty]
    private float? _downloadProgress;
    
    [ObservableProperty] private InstallationData? _installationData;
    
    [ObservableProperty] private string? _result;
    
    private readonly string _appDir;
    
    private readonly IApplicationModel _applicationModel;
    private readonly Downloader _downloader;
    private readonly ChecksumVerifier _checksumVerifier;

    public InstallationViewModel()
    {
        _applicationModel = new ApplicationGateway();
        _downloader = new Downloader();
        _checksumVerifier = new ChecksumVerifier();
        _appDir = GetApplicationDir();

        _ = PerformInstallation();
    }

    private async Task PerformInstallation()
    {
        var versionId = MainWindowViewModel.Instance.ApplicationVersion.ApplicationVersionId;
        InstallationData = await _applicationModel.GetInstallationData(versionId);

        var fileName = $"{MainWindowViewModel.Instance.SelectedApplication.ApplicationId}-{MainWindowViewModel.Instance.ApplicationVersion.VersionName}.raw";
        var installationDir = Path.Combine(_appDir, fileName); 
        
        await Download(installationDir);
        await VerifyChecksum(installationDir);
        await MockInstallation();
    }

    private async Task Download(string installationDir)
    {
        DownloadVisibility = Visibility.Visible;
        ChecksumVisibility = Visibility.Collapsed;
        InstallationVisibility = Visibility.Collapsed;
        
        var progress = new Progress<float>(p => DownloadProgress = p * 100f);
        
        Console.WriteLine($"Saving to file: {installationDir}");
        await _downloader.DownloadPackages(InstallationData!.DownloadUrl, installationDir, progress);
    }

    private async Task VerifyChecksum(string installationDir)
    {
        DownloadVisibility = Visibility.Collapsed;
        ChecksumVisibility = Visibility.Visible;
        
        var algorithm = InstallationData!.ChecksumAlgorithm;
        var checksum = InstallationData!.ChecksumHash;
        var result = await _checksumVerifier.VerifyAsync(installationDir, algorithm, checksum);
        
        Console.WriteLine("Hash verified: " + result);
    }
    
    private async Task MockInstallation()
    {
        ChecksumVisibility = Visibility.Collapsed;
        InstallationVisibility = Visibility.Visible;

        await Task.Delay(3000);
    }
    
    private static string GetApplicationDir()
    {
        var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        dir = Path.Combine(dir, "Premium-Installer");
        Directory.CreateDirectory(dir);
        return dir;
    }
}