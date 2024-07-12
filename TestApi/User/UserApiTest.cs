using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DAL.Data;
using BlazorEmpleados.DTOs.Empleado;
using BlazorEmpleados.DTOs.User;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Xunit;
using Program = BlazorEmpleados.API.Program;
namespace TestApi.User
{
    public class ApiUser : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;

        public ApiUser(WebApplicationFactory<Program> applicationFactory)
        {
            _applicationFactory = applicationFactory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove the existing DbContext registration.
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<EmpleadosDbContext>));
                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    // Add a database context (MyDbContext) using an in-memory database for testing.
                    services.AddDbContext<EmpleadosDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                    });

                    // Remove the existing IUserService registration.
                    var descriptorUser = services.SingleOrDefault(
                        d => d.ServiceType == typeof(IUserService));
                    if (descriptorUser != null)
                    {
                        services.Remove(descriptorUser);
                    }

                    // Add the stub service.
                    services.AddScoped<IUserService, StubServiceUser>();
                });
            });
        }

        [Fact]
        public async Task Test1_Login_Success()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();
            var loginRequest = new LoginRequestDTO
            {
                UserName = "testuserName",
                UserPassword = "testPassword"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Login/Login", loginRequest);

            // Assert
            response.EnsureSuccessStatusCode();
            var responseContent = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrEmpty(responseContent)); // Check that the token is not empty
        }

        //hacerlos como el primero para testear los endpoint, pq como esta ahora testea el service y no los endpoint
        //usar memoria cache para no afectar la db

        [Fact]
        public async Task Test2_Login_Failed()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();
            var loginRequest = new LoginRequestDTO
            {
                UserName = "xxxxx",
                UserPassword = "xyxyxyx"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Login/Login", loginRequest);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Test3_Register_Success()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();
            var userCreateRequest = new UserCreateRequestDTO
            {
                UserName = "testuser",
                UserPassword = "password",
                UserEmail = "testuser@example.com",
                UserPhone = "1234567890"
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Login/Register", userCreateRequest);
            response.EnsureSuccessStatusCode();
            var userResponse = await response.Content.ReadFromJsonAsync<UserResponseDTO>();

            // Assert
            Assert.NotNull(userResponse);
            Assert.Equal("testuser", userResponse.UserName);
            Assert.Equal("testuser@example.com", userResponse.UserEmail);
            Assert.Equal("password", userResponse.UserPassword);
            Assert.Equal("1234567890", userResponse.UserPhone);

        }

        [Fact]
        public async Task Test4_Register_Failed()
        {
            // Arrange
            var client = _applicationFactory.CreateClient();
            var userCreateRequest = new UserCreateRequestDTO
            {
                UserName = "aaa",
                UserPassword = "",
                UserEmail = "ac@gmail.com",
                UserPhone = ""
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/Login/Register", userCreateRequest);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
