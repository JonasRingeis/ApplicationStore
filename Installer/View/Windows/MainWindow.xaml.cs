using System.Windows.Media;
using Installer.ViewModel;
using Wpf.Ui;
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
        DataContext = App.GetRequiredService<MainWindowViewModel>();
        ApplicationThemeManager.Apply(this);
        
        var contentDialogService = App.GetRequiredService<IContentDialogService>();
        contentDialogService.SetDialogHost(RootContentDialogPresenter);
        
        OverwriteTheme();
    }

    private static void OverwriteTheme()
    {
        var accentColor = ColorConverter.ConvertFromString("#ff6c00");
        var accentHighlighted = ColorConverter.ConvertFromString("#ff7714");
        var accentClicked = ColorConverter.ConvertFromString("#ff8832");
        
        App.Current.Resources["SystemAccentColorPrimary"] = accentColor;
        App.Current.Resources["SystemAccentColorSecondary"] = accentHighlighted;
        App.Current.Resources["SystemAccentColorTertiary"] = accentClicked;
    }
}