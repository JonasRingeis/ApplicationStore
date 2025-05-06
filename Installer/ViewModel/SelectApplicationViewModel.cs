using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Installer.Model;
using ListView = Wpf.Ui.Controls.ListView;

namespace Installer.ViewModel;

public partial class SelectApplicationViewModel(IApplicationModel applicationModel) : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Application> _applications = [];

    [ObservableProperty] private bool _selectButtonEnabled;
    [ObservableProperty] private Application? _selectedApplication;

    public async Task GetAllApplications()
    {
        SelectButtonEnabled = false;
        Applications = new ObservableCollection<Application>(await applicationModel.GetAllApplications());
    }
    
    public void ApplicationClick(object sender, bool isDoubleClick)
    {
        SelectButtonEnabled = true;

        SelectedApplication = ((sender as ListView)!.SelectedItem as Application)!;
        
        if (!isDoubleClick) return;
        
        MainWindowViewModel.Instance.SelectedApplication = SelectedApplication;
        MainWindowViewModel.Instance.Navigate("SelectVersion");
    }

    [RelayCommand]
    private void SelectApp()
    {
        MainWindowViewModel.Instance.SelectedApplication = SelectedApplication!;
        MainWindowViewModel.Instance.Navigate("SelectVersion");
    }
}