using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Installer.ViewModel;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty] private string? _page;
    [ObservableProperty] private bool? _backButtonEnabled;
    
    public static MainWindowViewModel Instance => (MainWindowViewModel)System.Windows.Application.Current.MainWindow!.DataContext;

    private readonly Stack<string> _navigationStack = new();
    
    public MainWindowViewModel()
    {
        Navigate("SelectApplication");
    }

    public void Navigate(string page)
    {
        _navigationStack.Push(page);
        const string prefix = "../Pages/";
        Page = prefix + page + ".xaml";
        CalcBackVisibility();
    }

    [RelayCommand]
    private void Back()
    {
        _navigationStack.Pop();
        Navigate(_navigationStack.Pop());
        CalcBackVisibility();
    }

    private void CalcBackVisibility()
    {
        BackButtonEnabled = _navigationStack.Count > 1;
    }
}