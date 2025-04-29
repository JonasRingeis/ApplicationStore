using System.Windows;
using Wpf.Ui.Appearance;

namespace Installer.View.Windows;

public partial class SplashScreen : Window
{
    public SplashScreen()
    {
        InitializeComponent();
        ApplicationThemeManager.Apply(this);

        _ = LoadMainWindow();
    }

    private async Task LoadMainWindow()
    {
        // mock delay
        await Task.Delay(2000);
        
        var mainWindow = new MainWindow();
        while (mainWindow.IsLoaded)
        {
            await Task.Delay(100);
        }

        Close();
        mainWindow.Show();
    }
}