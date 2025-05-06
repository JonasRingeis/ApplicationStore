using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using Installer.Model;
using Installer.ViewModel.Installation;

namespace Installer.ViewModel;

public partial class InstallationViewModel(
    IApplicationModel applicationGateway,
    Downloader downloader,
    ChecksumVerifier checksumVerifier) : ObservableObject
{
    [ObservableProperty]
    private Visibility? _downloadVisibility, _checksumVisibility, _installationVisibility = Visibility.Collapsed;
    
    [ObservableProperty]
    private Visibility? _cancelButtonVisibility = Visibility.Visible;

    [ObservableProperty] private string? _downloadDomain;
    [ObservableProperty] private float? _downloadProgress;
    
    [ObservableProperty] private InstallationData? _installationData;
    
    [ObservableProperty] private string? _result;
    
    private readonly string _appDir = GetApplicationDir();

    public async Task PerformInstallation()
    {
        Reset();
        var versionId = MainWindowViewModel.Instance.ApplicationVersion.ApplicationVersionId;
        InstallationData = await applicationGateway.GetInstallationData(versionId);
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

    private void Reset()
    {
        DownloadVisibility  = Visibility.Collapsed;
        ChecksumVisibility  = Visibility.Collapsed;
        InstallationVisibility = Visibility.Collapsed;
        CancelButtonVisibility = Visibility.Visible;
        
        DownloadProgress = 0;
        Result = string.Empty;
    }

    private async Task Download(string installationDir)
    {
        DownloadVisibility = Visibility.Visible;
        ChecksumVisibility = Visibility.Collapsed;
        InstallationVisibility = Visibility.Collapsed;
        
        var progress = new Progress<float>(p => DownloadProgress = p * 100f);
        
        Console.WriteLine($"Saving to file: {installationDir}");
        await downloader.DownloadPackages(InstallationData!.DownloadUrl, installationDir, progress);
    }

    private async Task<bool> VerifyChecksum(string installationDir)
    {
        DownloadVisibility = Visibility.Collapsed;
        ChecksumVisibility = Visibility.Visible;
        
        var algorithm = InstallationData!.ChecksumAlgorithm;
        var checksum = InstallationData!.ChecksumHash;
        return await checksumVerifier.VerifyAsync(installationDir, algorithm, checksum);
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