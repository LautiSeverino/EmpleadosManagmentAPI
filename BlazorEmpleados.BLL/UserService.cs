using AutoMapper;
using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DAL.UnitOfWork.UnitOfWorkFolder;
using BlazorEmpleados.DTOs.User;
using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.BLL
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mapper = mapper;
        }
        public async Task<UserResponseDTO> Create(UserCreateRequestDTO user)
        {
            var userEntity = _mapper.Map<User>(user);
            await _unitOfWork.UserRepository.Create(userEntity);
            if (await _unitOfWork.Save() == 0)
                return null;
            var result = _mapper.Map<UserResponseDTO>(userEntity);
            return result;
        }

        public async Task<UserResponseDTO> GetUser(string userName, string password)
        {
            var usuario = await _unitOfWork.UserRepository.GetUser(userName, password);
            var result = _mapper.Map<UserResponseDTO>(usuario);
            if (usuario == null)
                return null;
            else
                return result;
                
        }
    }
}
