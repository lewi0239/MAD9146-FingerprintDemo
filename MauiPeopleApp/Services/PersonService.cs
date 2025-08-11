using System.Net.Http.Json;
using MauiPeopleApp.Models;
using System.Net.Http;

namespace MauiPeopleApp.Services;

public class PersonService
{
    private readonly HttpClient _httpClient;

    public PersonService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Person>> GetPeopleAsync()
    {
        var response = await _httpClient.GetFromJsonAsync<ApiResponse>(
            "https://reqres.in/api/users?api_key=reqres-free-v1"
        );
        Console.WriteLine($"Fetched {response?.Data?.Count ?? 0} people");
        return response?.Data ?? new List<Person>();
    }

    private class ApiResponse
    {
        public List<Person> Data { get; set; }
    }
}
