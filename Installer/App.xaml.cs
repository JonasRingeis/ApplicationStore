using Installer.Gateway;
using Installer.Model;
using Installer.ViewModel;
using Installer.ViewModel.Installation;
using Microsoft.Extensions.DependencyInjection;
using Wpf.Ui;
using Application = System.Windows.Application;

namespace Installer;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public new static App Current => (App)Application.Current; 
    
    public IServiceProvider Services;
    public App()
    {
        Services = ConfigureServices();
    }

    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Model
        services.AddSingleton<IApplicationModel, ApplicationGateway>();
        
        // Services
        services.AddSingleton<IContentDialogService, ContentDialogService>();
        
        // ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<SelectApplicationViewModel>();
        services.AddSingleton<InstallationViewModel>();
        services.AddSingleton<SelectVersionViewModel>();

        services.AddSingleton<ChecksumVerifier>();
        services.AddSingleton<Downloader>();
        
        return services.BuildServiceProvider();
    }

    public static T GetRequiredService<T>() where T : class
    {
        return Current.Services.GetRequiredService<T>();
    }
}