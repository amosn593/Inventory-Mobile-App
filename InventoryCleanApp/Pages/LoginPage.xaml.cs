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
        _authService.Login();
        await Shell.Current.GoToAsync($"//{nameof(Home)}");
    }
}