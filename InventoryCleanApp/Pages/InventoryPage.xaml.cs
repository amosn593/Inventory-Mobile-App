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
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is StoreViewModel vm)
            await vm.LoadProductsCommand.ExecuteAsync(null);
    }


}