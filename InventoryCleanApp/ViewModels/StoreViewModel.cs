using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryCleanApp.Models;
using InventoryCleanApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace InventoryCleanApp.ViewModels;

public partial class StoreViewModel : ObservableObject
{
    private const int PageSize = 10;
    private readonly AuthService _authService;

    
    [ObservableProperty]
    private ObservableCollection<Product> products = [];

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string searchQuery;

    [ObservableProperty]
    private int currentPage = 1;

    [ObservableProperty]
    private bool hasMoreItems;


    public StoreViewModel(AuthService authService)
    {
        _authService = authService;
        LoadProductsCommand = new AsyncRelayCommand(LoadProductsAsync);
        LoadProductsCommand.Execute(null); // Load on startup
        
    }

    public IAsyncRelayCommand LoadProductsCommand { get; }

    private async Task LoadProductsAsync()
    {
        if (IsBusy ) return;
        IsBusy = true;

        try
        {
            var httpClient = await _authService.GetAuthenticatedHttpclient();

            var productList = await httpClient.GetFromJsonAsync<List<Product>>("api/Product/GetProducts");

            Products.Clear();
            foreach (var item in productList)
                Products.Add(item);

            HasMoreItems = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"API Error: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private void LoadNextPage()
    {
        if (IsBusy || !HasMoreItems)
            return;
        CurrentPage++;
        _ = LoadProductsAsync();
    }

    partial void OnSearchQueryChanged(string value)
    {
        // Reset state on new search
        CurrentPage = 1;
        products.Clear();
        HasMoreItems = true;
        _ = LoadProductsAsync();
    }

    [RelayCommand]
    private void StockIn(Product item)
    {
        // Handle stock in logic
        Console.WriteLine($"Stock In: {item.Name}");
    }

    [RelayCommand]
    private void StockOut(Product item)
    {
        // Handle stock out logic
        Console.WriteLine($"Stock Out: {item.Name}");
    }
}
