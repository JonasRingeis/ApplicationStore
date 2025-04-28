using System.Windows.Controls;
using Installer.ViewModel;

namespace Installer.View.Pages;

public partial class SelectVersion : UserControl
{
    public SelectVersion()
    {
        InitializeComponent();
        DataContext = new SelectVersionViewModel();
    }
}