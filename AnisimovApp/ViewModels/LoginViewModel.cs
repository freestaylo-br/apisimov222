using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AnisimovApp.Services;
using AnisimovApp.Views;

namespace AnisimovApp.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly ApiService _apiService;

    public LoginViewModel()
    {
        _apiService = new ApiService();
    }

    [ObservableProperty]
    private string login = "";

    [ObservableProperty]
    private string password = "";

    [RelayCommand]
    private async Task LoginUser()
    {
        try
        {
            var user = await _apiService.Login(
                Login,
                Password);

            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ошибка",
                    "Неверный логин или пароль",
                    "OK");

                return;
            }

            await Application.Current.MainPage.DisplayAlert(
                "Успех",
                "Добро",
                "OK");


            Application.Current.MainPage =
                new NavigationPage(
                    new ProductsPage());
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert(
                "Ошибка",
                ex.Message,
                "OK");
        }
    }
}