using BlazorEmpleados.DTOs.User;
using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.BLL.Interface
{
    public interface IUserService
    {
        Task<UserResponseDTO> GetUser(string userName, string password);
        Task<UserResponseDTO> Create(UserCreateRequestDTO user);
    }
}
