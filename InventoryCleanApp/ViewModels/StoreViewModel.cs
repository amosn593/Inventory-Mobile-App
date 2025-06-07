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

    public StoreViewModel(AuthService authService)
    {
        _authService = authService;

    }

    public ObservableCollection<ProductModel> Products { get; set; } = [];

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? searchQuery;

    [ObservableProperty]
    private int currentPage = 1;

    [ObservableProperty]
    private bool hasMoreItems;

    [RelayCommand]
    public async Task LoadProductsAsync()
    {
        if (IsBusy) return;

        try
        {
            IsBusy = true;
            Products.Clear();
            var httpClient = await _authService.GetAuthenticatedHttpclient();

            var productList = await httpClient.GetFromJsonAsync<List<ProductModel>>("api/Product/GetProducts");

            foreach (var product in productList)
                Products.Add(product);

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
        //products.Clear();
        HasMoreItems = true;
        _ = LoadProductsAsync();
    }

    [RelayCommand]
    private void StockIn(ProductModel item)
    {
        // Handle stock in logic
        Console.WriteLine($"Stock In: {item.Name}");
    }

    [RelayCommand]
    private void StockOut(ProductModel item)
    {
        // Handle stock out logic
        Console.WriteLine($"Stock Out: {item.Name}");
    }
}
