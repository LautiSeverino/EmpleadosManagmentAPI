using BlazorEmpleados.DTOs.User;

namespace BlazorEmpleados.Client.Service.Interface
{
    public interface IUserService
    {
        Task<string> Login(LoginRequestDTO loginRequest);
        Task<UserResponseDTO> Register(UserCreateRequestDTO userCreateRequest);
    }
}
