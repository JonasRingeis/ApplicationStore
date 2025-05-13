using System.Collections;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.Input;

namespace Publisher_Ava.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly string _initialPage = "login";
    public MainWindowViewModel()
    {
        _currentPage = _pages[_initialPage];
    }

    private readonly PageViewModelBase _currentPage;
    private Stack<PageViewModelBase> _navigationStack = new();

    private readonly Dictionary<string, PageViewModelBase> _pages = new()
    {
        { "login", new LoginPageViewModel() }
    };


    [RelayCommand]
    private void NavigatePrevious()
    {
        
    }
}