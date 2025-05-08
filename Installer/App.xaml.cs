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

    /// <summary>
    /// Registers all services in a service collection
    /// </summary>
    /// <returns>Service provider built from service collection</returns>
    private IServiceProvider ConfigureServices()
    {
        var services = new ServiceCollection();

        // Model
        services.AddSingleton<IApplicationModel, ApplicationGateway>();
        
        // ViewModels
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<SelectApplicationViewModel>();
        services.AddSingleton<InstallationViewModel>();
        services.AddSingleton<SelectVersionViewModel>();

        services.AddSingleton<ChecksumVerifier>();
        services.AddSingleton<Downloader>();
        
        // Services
        services.AddSingleton<IContentDialogService, ContentDialogService>();
        
        return services.BuildServiceProvider();
    }

    /// <summary>
    /// Returns a service that was registered in the constructor
    /// </summary>
    /// <typeparam name="T">The type that is searched in the services collection</typeparam>
    /// <example>
    /// Constructor registering service
    /// <code>
    /// // ...
    /// services.AddSingleton&lt;SomeViewModel&gt;
    /// services.AddSingleton&lt;ISomeModel, SomeGateway&gt;
    /// // ...
    /// </code>
    ///
    /// Actual usage
    /// <code lang="csharp"> 
    /// SomeViewModel viewModel = App.GetRequiredService&lt;SomeViewModel&gt;();
    /// viewModel.DoSomething();
    ///
    /// ISomeModel model = App.GetRequiredService&lt;ISomeModel&gt;();
    /// model.GetSomeData();
    /// </code>
    /// </example>
    public static T GetRequiredService<T>() where T : class
    {
        return Current.Services.GetRequiredService<T>();
    }
}