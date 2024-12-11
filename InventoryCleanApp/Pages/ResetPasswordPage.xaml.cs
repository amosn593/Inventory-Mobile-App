using InventoryCleanApp.Services;

namespace InventoryCleanApp.Pages;

public partial class ResetPasswordPage : ContentPage
{
    private readonly AuthService _authService;
    public ResetPasswordPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    private void Clicked_logout_button(object sender, EventArgs e)
    {
        _authService.Logout();
        Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
    }
}