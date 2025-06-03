using InventoryCleanApp.Services;
using InventoryCleanApp.ViewModels;
using System.Collections.ObjectModel;

namespace InventoryCleanApp.Pages;

public partial class InventoryPage : ContentPage
{
   
    public InventoryPage(AuthService authService)
	{
		InitializeComponent();
        BindingContext = new StoreViewModel(authService);

    }

  
}