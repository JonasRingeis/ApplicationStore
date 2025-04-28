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
        
        var redColor = new Color
        {
            R = 240,
            G = 0,
            B = 32,
            A = 255
        };

        App.Current.Resources["SystemAccentColorPrimary"] = redColor;
    }
}