using AutoMapper;
using BlazorEmpleados.DTOs.Departamento;
using BlazorEmpleados.DTOs.Empleado;
using BlazorEmpleados.DTOs.User;
using BlazorEmpleados.Models;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.BLL.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Empleado, EmpleadoResponseDTO>()
            .ForMember(dest => dest.FechaContrato, opt => opt.MapFrom(src => src.FechaContrato.ToString("dd/MM/yyyy")));
            //.ForMember(dest => dest.Departamento, opt => opt.MapFrom(src => src.IdDepartamentoNavigation));
            CreateMap<EmpleadoCreateRequestDTO, Empleado>();

            CreateMap<Departamento, DepartamentoResponseDTO>();
            CreateMap<DepartamentoCreateDTO, Departamento>();

            CreateMap<User, UserResponseDTO>();
            CreateMap<UserCreateRequestDTO, User>();

        }
    }
}
