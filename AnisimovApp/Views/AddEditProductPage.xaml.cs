using System.Collections.ObjectModel;
using AnisimovApp.Models;
using AnisimovApp.Services;

namespace AnisimovApp.Views;

public partial class AddEditProductPage : ContentPage
{
    private readonly ApiService _apiService = new();

    public Product Product { get; set; }

    public Category SelectedCategory { get; set; }

    public Manufacturer SelectedManufacturer { get; set; }

    public Supplier SelectedSupplier { get; set; }

    public ObservableCollection<Category> Categories { get; set; }
        = new();

    public ObservableCollection<Manufacturer> Manufacturers { get; set; }
        = new();

    public ObservableCollection<Supplier> Suppliers { get; set; }
        = new();

    public AddEditProductPage()
    {
        InitializeComponent();

        Product = new Product();

        BindingContext = this;

        LoadData();
    }

    public AddEditProductPage(Product product)
    {
        InitializeComponent();

        Product = product;

        BindingContext = this;

        LoadData();
    }

    private async void LoadData()
    {
        var categories =
            await _apiService.GetCategories();

        foreach (var category in categories)
        {
            Categories.Add(category);
        }

        var manufacturers =
            await _apiService.GetManufacturers();

        foreach (var manufacturer in manufacturers)
        {
            Manufacturers.Add(manufacturer);
        }

        var suppliers =
            await _apiService.GetSuppliers();

        foreach (var supplier in suppliers)
        {
            Suppliers.Add(supplier);
        }
    }

    private async void Save_Clicked(
    object sender,
    EventArgs e)
    {
        var category =
            CategoryPicker.SelectedItem as Category;

        var manufacturer =
            ManufacturerPicker.SelectedItem as Manufacturer;

        var supplier =
            SupplierPicker.SelectedItem as Supplier;

        if (category == null ||
            manufacturer == null ||
            supplier == null)
        {
            await DisplayAlert(
                "Ошибка",
                "Выберите категорию, производителя и поставщика",
                "OK");

            return;
        }

        Product.CategoryId =
            category.CategoryId;

        Product.ManufacturerId =
            manufacturer.ManufacturerId;

        Product.SupplierId =
            supplier.SupplierId;

        var dto = new ProductDto
        {
            ProductName = Product.ProductName,
            Description = Product.Description,
            Amount = Product.Amount,
            Discount = Product.Discount,
            Count = Product.Count,
            UnitOfMeasurement = Product.UnitOfMeasurement,
            Article = Product.Article,

            CategoryId = category.CategoryId,
            ManufacturerId = manufacturer.ManufacturerId,
            SupplierId = supplier.SupplierId
        };

        var result =
            await _apiService.AddProduct(dto);

        if (result)
        {
            await DisplayAlert(
                "Успех",
                "Товар сохранён",
                "OK");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert(
                "Ошибка",
                "Не удалось сохранить товар",
                "OK");
        }
    }
}