using AnisimovApp.ViewModels;
using AnisimovApp.Services;
namespace AnisimovApp.Views;


public partial class ProductsPage : ContentPage
{
	public ProductsPage()
	{
		InitializeComponent();

		BindingContext = new ProductsViewModel();
	}

private void Logout_Clicked(object sender, EventArgs e) 
	{
		Session.UserFio = "";
		Application.Current.MainPage = new NavigationPage(new LoginPage());
	}
}