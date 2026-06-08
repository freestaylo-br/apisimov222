using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using AnisimovApp.Models;
using AnisimovApp.Services;

namespace AnisimovApp.ViewModels;

public partial class ProductsViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    public ObservableCollection<Product> Products { get; set; }
        = new();

    public ObservableCollection<Supplier> Suppliers { get; set; }
        = new();

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private Supplier selectedSupplier;

    [ObservableProperty]
    private bool isAscending = true;

    [ObservableProperty]
    private bool isDescending;

    public ProductsViewModel()
    {
        _apiService = new ApiService();

        LoadSuppliers();
        LoadProducts();
    }

    private async void LoadProducts()
    {
        List<Product> products;

        if (IsDescending)
        {
            products = await _apiService.GetProductsDesc();
        }
        else
        {
            products = await _apiService.GetProductsAsc();
        }

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            products = products.Where(x =>
                x.ProductName.Contains(
                    SearchText,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        if (SelectedSupplier != null && SelectedSupplier.SupplierId != 0)
        {
            products = products
                .Where(x => x.Supplier == SelectedSupplier.SupplierName).ToList();
        }

        Products.Clear();

        foreach (var product in products)
        {
            Products.Add(product);
        }
    }

    private async void LoadSuppliers()
    {
        var suppliers =
            await _apiService.GetSuppliers();

        Suppliers.Clear();

        Suppliers.Add(new Supplier
        {
            SupplierId = 0,
            SupplierName = "Все поставщики"
        });

        foreach (var supplier in suppliers)
        {
            Suppliers.Add(supplier);
        }

        SelectedSupplier = Suppliers[0];
    }

    partial void OnSearchTextChanged(string value)
    {
        LoadProducts();
    }

    partial void OnIsAscendingChanged(bool value)
    {
        if (value)
        {
            IsDescending = false;
            LoadProducts();
        }
    }

    partial void OnIsDescendingChanged(bool value)
    {
        if (value)
        {
            IsAscending = false;
            LoadProducts();
        }
    }

    public string UserFio =>
        string.IsNullOrWhiteSpace(Session.UserFio)
            ? "Гость"
            : Session.UserFio;

    partial void OnSelectedSupplierChanged(Supplier value)
    {
        LoadProducts();
    }
}