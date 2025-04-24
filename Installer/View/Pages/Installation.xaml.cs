using System.Windows;
using System.Windows.Controls;
using Installer.ViewModel;

namespace Installer.View;

public partial class Installation : UserControl
{
    public Installation()
    {
        InitializeComponent();
        DataContext = new InstallationViewModel();
    }

    public void Button_Click(object sender, RoutedEventArgs e)
    {
        
    }
}