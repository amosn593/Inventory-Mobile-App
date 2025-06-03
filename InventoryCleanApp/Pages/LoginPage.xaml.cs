using InventoryCleanApp.Models;
using InventoryCleanApp.Services;
using InventoryCleanApp.ViewModels;

namespace InventoryCleanApp.Pages;

public partial class LoginPage : ContentPage
{
    //private readonly AuthService _authService;
    public LoginPage(AuthService authService)
	{
		InitializeComponent();
        BindingContext = new LoginViewModel(authService);
        //_authService = authService;
    }

    //private async void Clicked_login_button(object sender, EventArgs e)
    //{
    //    var response = await _authService.Login(new Login {Email="", Password= "Joshua@2020" });
    //    if (string.IsNullOrWhiteSpace(response))
    //    {
    //        await Shell.Current.GoToAsync($"//{nameof(Home)}");
    //    }
    //    else
    //    {
    //        await Shell.Current.DisplayAlert("Error", response, "Ok");
    //    }
        
    //}
}