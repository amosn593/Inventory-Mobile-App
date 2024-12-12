using InventoryCleanApp.Models;
using InventoryCleanApp.Services;

namespace InventoryCleanApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly AuthService _authService;
    public LoginPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    private async void Clicked_login_button(object sender, EventArgs e)
    {
        var response = await _authService.Login(new Login {Email= "amungata@kcaa.or.ke", Password= "Joshua@2020" });
        if (string.IsNullOrWhiteSpace(response))
        {
            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", response, "Ok");
        }
        
    }
}