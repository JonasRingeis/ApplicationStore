using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Publisher_Ava.ViewModels;

public partial class LoginPageViewModel : PageViewModelBase
{
    public override bool CanNavigatePrevious { get; protected set; }

    [ObservableProperty]
    private string? _username;
    
    [ObservableProperty]
    private string? _password;

    [RelayCommand]
    private void Login()
    {
        Console.WriteLine($"Login with data: username: '{Username}', pwd: '{Password}'");
    }
}