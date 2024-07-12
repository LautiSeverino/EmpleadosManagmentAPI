using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DAL.Data;
using BlazorEmpleados.DTOs.Departamento;
using BlazorEmpleados.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Program = BlazorEmpleados.API.Program;
namespace TestApi.Departamento
{
    public class DepartamentoApiTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public DepartamentoApiTest(WebApplicationFactory<Program> factory)
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
                    var descriptorDepartamento = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IDepartamentoService));
                    if (descriptorDepartamento != null)
                    {
                        services.Remove(descriptorDepartamento);
                    }

                    // Add the stub service.
                    services.AddScoped<IDepartamentoService, StubServiceDepartamento>();

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
        public async Task Tets1_GetAll_Success()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Act
            var response = await client.GetAsync("/api/Departamento/GetAll");
            response.EnsureSuccessStatusCode();
            var departamentos = await response.Content.ReadFromJsonAsync<List<DepartamentoResponseDTO>>();

            //Assert
            Assert.NotNull(departamentos);
            Assert.Equal(3, departamentos.Count);
            Assert.Equal("Recursos Humanos", departamentos[0].Nombre);
            Assert.Equal(1, departamentos[0].IdDepartamento);

            Assert.Equal("Desarrollo", departamentos[1].Nombre);
            Assert.Equal(2, departamentos[1].IdDepartamento);

            Assert.Equal("Logistica", departamentos[2].Nombre);
            Assert.Equal(3, departamentos[2].IdDepartamento);

        }

        [Fact]
        public async Task Tets2_Create_Success()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var departamentoCreate = new DepartamentoCreateDTO
            {
                Nombre = "Finanzas"
            };

            //Act
            var response = await client.PostAsJsonAsync("/api/Departamento/Create", departamentoCreate);
            response.EnsureSuccessStatusCode();
            var departamentoResponse = await response.Content.ReadFromJsonAsync<DepartamentoResponseDTO>();

            //Assert
            Assert.NotNull(departamentoResponse);
            Assert.Equal("Finanzas", departamentoCreate.Nombre);

        }

        [Fact]
        public async Task Tets3_Create_Failed()
        {
            //Arrange
            var client = _factory.CreateClient();
            var token = GenerateJwtToken();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var departamentoCreate = new DepartamentoCreateDTO
            {
                Nombre = ""
            };

            //Act
            var response = await client.PostAsJsonAsync("/api/Departamento/Create", departamentoCreate);

            //Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        
    }
}
