using InventoryCleanApp.Services;

namespace InventoryCleanApp.Pages;

public partial class LoadingPage : ContentPage
{
    private readonly AuthService _authService;

    public LoadingPage(AuthService authService)
	{
		InitializeComponent();
        _authService = authService;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        if (await _authService.IsAuthenticated())
        {
            await Shell.Current.GoToAsync($"//{nameof(Home)}");
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}