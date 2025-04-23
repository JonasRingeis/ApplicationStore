using System.Windows.Controls;
using Installer.ViewModel;

namespace Installer.View;

public partial class SelectApplication : UserControl
{
    public SelectApplication()
    {
        InitializeComponent();
        DataContext = new SelectApplicationViewModel();
    }
}