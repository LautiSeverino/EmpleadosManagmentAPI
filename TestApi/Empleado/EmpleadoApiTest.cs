using BlazorEmpleados.DAL.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Program = BlazorEmpleados.API.Program;
using BlazorEmpleados.BLL.Interface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BlazorEmpleados.DTOs.Empleado;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace TestApi.Empleado
{
    public class EmpleadoApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public EmpleadoApiTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    //Remove
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<EmpleadosDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    //Add
                    services.AddDbContext<EmpleadosDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    //Remove
                    var descriptorEmpleado = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IEmpleadoService));
                    if (descriptorEmpleado != null)
                    {
                        services.Remove(descriptorEmpleado);
                    }

                    // Add the stub service.
                    services.AddScoped<IEmpleadoService, StubServiceEmpleado>();

                    //Configure JWT Auth For Testing
                    services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", options => { });
                });
            });
        }

        private string GenerateJwtToken()
        {
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, "testuser"),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, "testuser")
            };

            // Use a key with at least 32 characters
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("xxxxVeryLongSecretKeyWith32Charactzzzz"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourissuer",
                audience: "youraudience",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [Fact]
        public async Task Test1_CreateEmpleado_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoCreateRequest = new EmpleadoCreateRequestDTO
            {
                NombreCompleto = "John Doe",
                Sueldo = 50000,
                NroDocumento = "12345678",
                IdDepartamento = 1,
                FechaContrato = DateTime.Now
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Empleado/Create", empleadoCreateRequest);
            response.EnsureSuccessStatusCode();
            var empleadoResponse = await response.Content.ReadFromJsonAsync<EmpleadoResponseDTO>();

            // Assert
            Assert.NotNull(empleadoResponse);
            Assert.Equal("John Doe", empleadoResponse.NombreCompleto);
            Assert.Equal(50000, empleadoResponse.Sueldo);
            Assert.Equal("12345678", empleadoResponse.NroDocumento);
            Assert.Equal(1, empleadoResponse.IdDepartamento);
        }

        [Fact]
        public async Task Test2_CreateEmpleado_Failed()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoCreateRequest = new EmpleadoCreateRequestDTO
            {
                NombreCompleto = "John Doe",
                Sueldo = 50000,
                NroDocumento = "",
                IdDepartamento = 1,
                FechaContrato = DateTime.Now
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Empleado/Create", empleadoCreateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }


        [Fact]
        public async Task Test3_GetAll_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await client.GetAsync("/api/Empleado/GetAll");
            response.EnsureSuccessStatusCode();
            var empleados = await response.Content.ReadFromJsonAsync<List<EmpleadoResponseDTO>>();

            // Assert
            Assert.NotNull(empleados);
            Assert.Equal(2, empleados.Count);
            Assert.Equal("John Doe", empleados[0].NombreCompleto);
            Assert.Equal(50000, empleados[0].Sueldo);
            Assert.Equal("12345678", empleados[0].NroDocumento);
            Assert.Equal(1, empleados[0].IdDepartamento);

            Assert.Equal("Jane Smith", empleados[1].NombreCompleto);
            Assert.Equal(60000, empleados[1].Sueldo);
            Assert.Equal("87654321", empleados[1].NroDocumento);
            Assert.Equal(2, empleados[1].IdDepartamento);
        }

        [Fact]
        public async Task Test4_GetByNombre_Success()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoNombreRequest = "j";

            //Act
            var response = await client.GetAsync($"/api/Empleado/GetByNombre/{empleadoNombreRequest}");
            response.EnsureSuccessStatusCode();
            var empleados = await response.Content.ReadFromJsonAsync<List<EmpleadoResponseDTO>>();

            // Assert
            Assert.NotNull(empleados);
            Assert.Equal(2, empleados.Count);
            Assert.Equal("John Doe", empleados[0].NombreCompleto);
            Assert.Equal(50000, empleados[0].Sueldo);
            Assert.Equal("12345678", empleados[0].NroDocumento);
            Assert.Equal(1, empleados[0].IdDepartamento);

            Assert.Equal("Jane Smith", empleados[1].NombreCompleto);
            Assert.Equal(60000, empleados[1].Sueldo);
            Assert.Equal("87654321", empleados[1].NroDocumento);
            Assert.Equal(2, empleados[1].IdDepartamento);
        }

        [Fact]
        public async Task Test5_GetByNombre_Failed()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoNombreRequest = "xxx";

            //Act
            var response = await client.GetAsync($"/api/Empleado/GetByNombre/{empleadoNombreRequest}");

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test6_GetByNroDocumentoe_Success()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoDocumentoRequest = "12345678";

            //Act
            var response = await client.GetAsync($"/api/Empleado/GetByNroDocumento/{empleadoDocumentoRequest}");
            var empleado = await response.Content.ReadFromJsonAsync<EmpleadoResponseDTO>();

            // Assert
            Assert.NotNull(empleado);
            Assert.Equal("John Doe", empleado.NombreCompleto);
            Assert.Equal(50000, empleado.Sueldo);
            Assert.Equal("12345678", empleado.NroDocumento);
            Assert.Equal(1, empleado.IdDepartamento);
        }

        [Fact]
        public async Task Test7_GetByNroDocumentoe_Failed()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoDocumentoRequest = "1234";

            //Act
            var response = await client.GetAsync($"/api/Empleado/GetByNroDocumento/{empleadoDocumentoRequest}");

            // Assert
            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test8_Update_Success()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoUpdateRequest = new EmpleadoUpdateRequestDTO
            {
                NombreCompleto = "Juan Perez",
                IdDepartamento = 1,
                Sueldo = 200,
                NroDocumento = "12345678",
                FechaContrato = DateTime.Now
            };

            //Act
            var response = await client.PutAsJsonAsync($"/api/Empleado/Update/{empleadoUpdateRequest.NroDocumento}", empleadoUpdateRequest);
            response.EnsureSuccessStatusCode();
            var empleado = await response.Content.ReadFromJsonAsync<EmpleadoResponseDTO>();

            // Assert
            Assert.NotNull(empleado);
            Assert.Equal("Juan Perez", empleado.NombreCompleto);
            Assert.Equal(200, empleado.Sueldo);
            Assert.Equal("12345678", empleado.NroDocumento);
            Assert.Equal(1, empleado.IdDepartamento);
        }

        [Fact]
        public async Task Test9_Update_Failed()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoUpdateRequest = new EmpleadoUpdateRequestDTO
            {
                NombreCompleto = "Juan Perez",
                IdDepartamento = 1,
                Sueldo = 200,
                NroDocumento = "12345",
                FechaContrato = DateTime.Now
            };

            //Act
            var response = await client.PutAsJsonAsync($"/api/Empleado/Update/{empleadoUpdateRequest.NroDocumento}", empleadoUpdateRequest);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Test10_Delete_Success()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoDocumentoRequest = "12345678";

            // Act
            var response = await client.DeleteAsync($"/api/Empleado/Delete/{empleadoDocumentoRequest}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task Test11_Delete_Failed()
        {
            // Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var empleadoDocumentoRequest = "0000";

            // Act
            var response = await client.DeleteAsync($"/api/Empleado/Delete/{empleadoDocumentoRequest}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}
