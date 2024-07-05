using BlazorEmpleados.Client.Service.Interface;
using BlazorEmpleados.DAL.Data.Migrations;
using BlazorEmpleados.DTOs.Departamento;
using System.Net.Http.Json;

namespace BlazorEmpleados.Client.Service
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly HttpClient _httpClient;
        public DepartamentoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Create(DepartamentoCreateDTO departamento)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Departamento/Create", departamento);
            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Departamento/Delete/{id}");
            var response = await result.Content.ReadFromJsonAsync<bool>();
            if (response == true)
            {
                return true;
            }
            return false;
        }

        public async Task<List<DepartamentoResponseDTO>> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<DepartamentoResponseDTO>>("api/Departamento/GetAll");
            return response;
        }
    }
}
