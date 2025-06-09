using InventoryCleanApp.ViewModels;

namespace InventoryCleanApp.Pages;

public partial class InventoryPage : ContentPage
{
    private readonly StoreViewModel _storeViewModel;

    public InventoryPage(StoreViewModel storeViewModel)
	{
		InitializeComponent();
        _storeViewModel = storeViewModel;
        BindingContext = _storeViewModel;
        //Initialized();
    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    await _storeViewModel.InitAsync();
    //}

    //private async Task Initialized()
    //{
    //    await _storeViewModel.InitAsync();
    //}


}