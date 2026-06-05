using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using AnisimovApp.Models;
using AnisimovApp.Services;

namespace AnisimovApp.ViewModels;

public partial class ProductsViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    private List<Product> allProducts = new();

    [ObservableProperty]
    private string searchText = "";

    public ObservableCollection<Product> Products { get; set; }
        = new();

    public ProductsViewModel()
    {
        _apiService = new ApiService();

        LoadProducts();
    }

    private async void LoadProducts()
    {
        allProducts = await _apiService.GetProducts();

        Products.Clear();

        foreach (var product in allProducts)
        {
            Products.Add(product);
        }
    }

    partial void OnSearchTextChanged(string value)
    {
        Products.Clear();

        var filtered = allProducts.Where(x =>
            x.ProductName.Contains(
                value ?? "",
                StringComparison.OrdinalIgnoreCase));

        foreach (var product in filtered)
        {
            Products.Add(product);
        }
    }
}