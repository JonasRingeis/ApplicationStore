using System.Windows;
using System.Windows.Controls;
using Installer.ViewModel;

namespace Installer.View.UserControls;

public partial class ChecksumFailed : UserControl
{
    public ChecksumFailed()
    {
        InitializeComponent();
    }

    private void Close_Click(object sender, RoutedEventArgs e)
    {
        App.Current.MainWindow.Close();
    }
    private void OtherVersion_Click(object sender, RoutedEventArgs e)
    {
        MainWindowViewModel.Instance.BackCommand.Execute(sender);
    }
}