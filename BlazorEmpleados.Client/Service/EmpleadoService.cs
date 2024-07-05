using BlazorEmpleados.Client.Service.Interface;
using BlazorEmpleados.DTOs.Empleado;
using System.Net;
using System.Net.Http.Json;

namespace BlazorEmpleados.Client.Service
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly HttpClient _httpClient;
        public EmpleadoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Create(EmpleadoCreateRequestDTO empleado)
        {
            var result = await _httpClient.PostAsJsonAsync("api/Empleado/Create", empleado);

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Delete(string nroDocumento)
        {
            var result = await _httpClient.DeleteAsync($"api/Empleado/Delete/{nroDocumento}");
            var response = await result.Content.ReadFromJsonAsync<bool>();
            if(response == true)
            {
                return true;
            }
            return false;
        }

        public async Task<List<EmpleadoResponseDTO>> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<List<EmpleadoResponseDTO>>("api/Empleado/GetAll");
            return response;
        }

        public async Task<List<EmpleadoResponseDTO>> GetByNombre(string nombre)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<List<EmpleadoResponseDTO>>($"api/Empleado/GetByNombre/{nombre}");
                return response ?? new List<EmpleadoResponseDTO>();
            }
            catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                // Retornar lista vacía en caso de no encontrar empleados
                return new List<EmpleadoResponseDTO>();
            }
        }

        public async Task<EmpleadoResponseDTO> GetByNroDocumento(string nroDocumento)
        {
            try
            {
                var response = await _httpClient.GetFromJsonAsync<EmpleadoResponseDTO>($"api/Empleado/GetByNroDocumento/{nroDocumento}");

                return response;
            }
            catch (HttpRequestException ex) when(ex.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<bool> Update(EmpleadoUpdateRequestDTO empleado, string nroDocumento)
        {
            empleado.NroDocumento = nroDocumento;
            var result = await _httpClient.PutAsJsonAsync($"api/Empleado/Update/{nroDocumento}", empleado);
            return result.IsSuccessStatusCode;
        }
    }
}
