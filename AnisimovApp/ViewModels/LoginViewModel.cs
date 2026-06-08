using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using AnisimovApp.Services;
using AnisimovApp.Views;
using System.Runtime.Intrinsics.X86;

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

            if (user==null)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Ошибка",
                    "Неверный логин или пароль",
                    "ОК");

                return;
            }

            Session.UserFio = $"{user.Surname} {user.Name} {user.Patronymic}";

            

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

    [RelayCommand]
    private async Task GuestLogin()
    {
        Session.UserFio = "Гость";

        await Application.Current.MainPage.Navigation.PushAsync(new ProductsPage());
    }
}