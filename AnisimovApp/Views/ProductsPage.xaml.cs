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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ProductsViewModel vm)
        {
            vm.LoadProducts();
        }
    }

    private async void AddProduct_Clicked(
        object sender,
        EventArgs e)
    {
		if (AddEditProductPage.IsOpened)
        {
            await DisplayAlert Alert(
                "Внимание",
                "Окно редактирования уже открыто",
                "ОК");
            return;
        }

        AddEditProductPage.IsOpened = true;

        await Navigation.PushAsync(new AddEditProductPage(product));
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        IsOpened = false;
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