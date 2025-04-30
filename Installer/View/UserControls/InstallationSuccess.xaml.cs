using System.Windows;
using System.Windows.Controls;
using Installer.ViewModel;

namespace Installer.View.UserControls;

public partial class InstallationSuccess : UserControl
{
    public InstallationSuccess()
    {
        InitializeComponent();
    }

    private void InstallMore_Click(object sender, RoutedEventArgs e)
    {
        MainWindowViewModel.Instance.BackCommand.Execute(null);
        MainWindowViewModel.Instance.BackCommand.Execute(null);
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        App.Current.MainWindow.Close();
    }
}