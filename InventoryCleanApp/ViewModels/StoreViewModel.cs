using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InventoryCleanApp.Models;
using InventoryCleanApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InventoryCleanApp.ViewModels;

public partial class StoreViewModel : ObservableObject
{
    private readonly ProductRepo _productRepo;

    public StoreViewModel(ProductRepo productRepo)
    {
        _productRepo = productRepo;
        LoadProductsCommand.Execute(null); // Initial load
       
    }

    public ObservableCollection<ProductModel> Products { get; set; } = [];

    private int _page = 1;
    private const int PageSize = 10;
    private bool _isLastPage = true;


    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private string? searchQuery;

    [ObservableProperty] 
    private bool isRefreshing;

    [RelayCommand]
    public async Task LoadProducts()
    {
        if (IsBusy) return;
        try
        {
            IsBusy = true;
            _page = 1;
            _isLastPage = false;
            Products.Clear();
            var newRecords = await _productRepo.GetProductsAsync(_page, SearchQuery);
            //Debug.WriteLine($"Loaded {newRecords.Items.Count} products on page {_page}");
            //await Shell.Current.DisplayAlert("Error", "Failed to load products. Please try again later.", "OK");
            //Products = new ObservableCollection<ProductModel>(newRecords.Items);
            //Products = newRecords.Items;
            foreach (var record in newRecords.Items)
            {

                Products.Add(record);
            }
            if (newRecords.MetaData.HasNext)
                _isLastPage = false;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading products: {ex.Message}");
            await Shell.Current.DisplayAlert("Error", "Failed to load products. Please try again later.", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task Refresh()
    {
        IsRefreshing = true;
        await LoadProducts();
        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task LoadMore()
    {
        if (IsBusy || _isLastPage) return;
        try
        {
            IsBusy = true;
            _page++;
            var moreRecords = await _productRepo.GetProductsAsync(_page, SearchQuery);

            //Products.ToArray().Add; (moreRecords.Items);

            foreach (var record in moreRecords.Items)
            {
                Products.Add(record);
            }
                

            if (moreRecords.MetaData.HasNext)
                _isLastPage = false;
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        await LoadProducts();
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
