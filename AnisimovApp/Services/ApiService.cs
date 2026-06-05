using System.Net.Http.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnisimovApp.Models;

namespace AnisimovApp.Services;

public class ApiService
{
    private readonly HttpClient _client;

    public ApiService()
    {
        _client = new HttpClient();

        _client.BaseAddress =
            new Uri("http://localhost:5282/api/");
    }

    public async Task<LoginResponse?> Login(
        string login,
        string password)
    {
        var request = new LoginRequest
        {
            Login = login,
            Password = password
        };

        var response =
            await _client.PostAsJsonAsync(
                "Auth/login",
                request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content
            .ReadFromJsonAsync<LoginResponse>();
    }

    public async Task<List<Product>> GetProducts()
    {
        var products =
            await _client.GetFromJsonAsync<List<Product>>(
                "Products");

        return products ?? new List<Product>();
    }
}
