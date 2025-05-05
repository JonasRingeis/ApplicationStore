using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Installer.View.UserControls;

public partial class EulaDialog : ContentDialog
{
    public EulaDialog(ContentPresenter? contentPresenter) : base(contentPresenter)
    {
        InitializeComponent();
    }
}