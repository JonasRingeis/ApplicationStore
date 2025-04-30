using System.IO;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
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
    private Visibility? _cancelButtonVisibility = Visibility.Visible;

    [ObservableProperty] private string? _downloadDomain;
    [ObservableProperty] private float? _downloadProgress;
    
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
        DownloadDomain = DomainExtractionRegex().Match(InstallationData.DownloadUrl).Groups["domain"].Value;

        var fileName = $"{MainWindowViewModel.Instance.SelectedApplication.ApplicationId}-{MainWindowViewModel.Instance.ApplicationVersion.VersionName}.raw";
        var installationDir = Path.Combine(_appDir, fileName); 
        
        await Download(installationDir);
        var verified = await VerifyChecksum(installationDir);
        if (!verified)
        {
            ShowChecksumFailed();
            return;
        }
        await MockInstallation();
        ShowSuccess();
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

    private async Task<bool> VerifyChecksum(string installationDir)
    {
        DownloadVisibility = Visibility.Collapsed;
        ChecksumVisibility = Visibility.Visible;
        
        var algorithm = InstallationData!.ChecksumAlgorithm;
        var checksum = InstallationData!.ChecksumHash;
        return await _checksumVerifier.VerifyAsync(installationDir, algorithm, checksum);
    }
    
    private async Task MockInstallation()
    {
        ChecksumVisibility = Visibility.Collapsed;
        InstallationVisibility = Visibility.Visible;

        await Task.Delay(1000);
    }

    private void ShowChecksumFailed()
    {
        HideInstallationUi();
        Result = "../UserControls/ChecksumFailed.xaml";        
    }
    private void ShowSuccess()
    {
        HideInstallationUi();
        Result = "../UserControls/InstallationSuccess.xaml";
    }
    private void HideInstallationUi()
    {
        ChecksumVisibility = Visibility.Collapsed;
        InstallationVisibility = Visibility.Collapsed;
        CancelButtonVisibility = Visibility.Collapsed;
    }
    
    private static string GetApplicationDir()
    {
        var dir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        dir = Path.Combine(dir, "Premium-Installer");
        Directory.CreateDirectory(dir);
        return dir;
    }

    [GeneratedRegex(@"^(http[s]?\:\/\/)?(?<domain>[a-zA-Z\.]+)\/.+")]
    private static partial Regex DomainExtractionRegex();
}