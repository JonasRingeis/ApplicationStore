using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Publisher_Ava.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly string _initialPage = "login";
    public MainWindowViewModel()
    {
        _currentPage = _pages[_initialPage];
    }

    [ObservableProperty]
    private PageViewModelBase _currentPage;
    
    private readonly Stack<PageViewModelBase> _navigationStack = new();

    private readonly Dictionary<string, PageViewModelBase> _pages = new()
    {
        { "login", new LoginPageViewModel() }
    };


    [RelayCommand]
    private void NavigatePrevious()
    {
        if (!CurrentPage.CanNavigatePrevious) return;
        
        _navigationStack.Pop();
        CurrentPage = _navigationStack.Peek();
    }

    [RelayCommand]
    private void NavigateTo(string pageName)
    {
        CurrentPage = _pages[pageName];
        _navigationStack.Push(CurrentPage);
    }
}