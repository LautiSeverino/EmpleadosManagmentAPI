using BlazorEmpleados.Client.Service.Interface;
using BlazorEmpleados.DTOs.User;
using Blazored.LocalStorage;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorEmpleados.Client.Service
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public UserService(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<string> Login(LoginRequestDTO loginRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Login/Login", loginRequest);
            if (response.IsSuccessStatusCode)
            {
                var token = await response.Content.ReadAsStringAsync();
                await _localStorage.SetItemAsync("authToken", token);
                return token;
            }

            return null;
        }

        public async Task<UserResponseDTO> Register(UserCreateRequestDTO userCreateRequest)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Login/Register", userCreateRequest);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserResponseDTO>();
            }

            return null;
        }
    }
}
