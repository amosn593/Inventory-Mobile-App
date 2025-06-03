using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryCleanApp.Models;
using InventoryCleanApp.Pages;
using InventoryCleanApp.Services;

namespace InventoryCleanApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly AuthService _authService;
    public LoginViewModel(AuthService authService)
    {
        _authService = authService;
    }
    
    [ObservableProperty]
    private Login login = new();     

    [RelayCommand]
    private async Task LoginUser()
    {
        Console.WriteLine("Button clicked!");
        var response = await _authService.Login(new Login { Email = Login.Email, Password = Login.Password });
        if (string.IsNullOrWhiteSpace(response))
        {
            Login = new();
            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", response, "Ok");
        }
    }
}
