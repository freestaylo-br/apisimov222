using AnisimovApp.ViewModels;
using AnisimovApp.Services;
using AnisimovApp.Models;

namespace AnisimovApp.Views;


public partial class ProductsPage : ContentPage
{
	public ProductsPage()
	{
		InitializeComponent();

		BindingContext = new ProductsViewModel();
	}

    private async void AddProduct_Clicked(
        object sender,
        EventArgs e)
    {
		await Navigation.PushAsync(new AddEditProductPage());
    }

    private void Logout_Clicked(object sender, EventArgs e) 
	{
		Session.UserFio = "";
		Application.Current.MainPage = new NavigationPage(new LoginPage());
	}

    private async void Product_Selected(
    object sender,
    SelectionChangedEventArgs e)
    {
        if (Session.UserRole != "Администратор")
            return;

        var product =
            e.CurrentSelection.FirstOrDefault()
            as Product;

        if (product == null)
            return;

        await Navigation.PushAsync(
            new AddEditProductPage(product));

        ((CollectionView)sender).SelectedItem = null;
    }
}