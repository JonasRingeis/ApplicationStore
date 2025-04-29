using System.Windows.Media;
using Installer.ViewModel;
using Wpf.Ui.Appearance;

namespace Installer.View.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowViewModel();
        ApplicationThemeManager.Apply(this);
        
        var accentColor = ColorConverter.ConvertFromString("#ff6c00");
        App.Current.Resources["SystemAccentColorPrimary"] = accentColor;
    }
}