using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.User
{
    public class StubServiceUser : IUserService
    {
        public async Task<UserResponseDTO> Create(UserCreateRequestDTO user)
        {
            return await Task.FromResult(new UserResponseDTO
            {
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                UserPassword = user.UserPassword,
                UserPhone = user.UserPhone
            });
        }

        public async Task<UserResponseDTO> GetUser(string userName, string password)
        {
            if (userName == "testuserName" && password == "testPassword")
            {
                return await Task.FromResult(new UserResponseDTO
                {
                    UserName = "testuserName",
                    UserEmail = "testuser@gmail.com",
                    UserPassword = "testPassword",
                    UserPhone = "999666999"
                });
            }
            else
                return await Task.FromResult<UserResponseDTO>(null);
        }
    }
}
