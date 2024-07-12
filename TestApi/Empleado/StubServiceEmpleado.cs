using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DTOs.Empleado;
using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestApi.Empleado
{
    public class StubServiceEmpleado : IEmpleadoService
    {
        private List<EmpleadoResponseDTO> _empleados = new List<EmpleadoResponseDTO>
    {
        new EmpleadoResponseDTO
        {
            NombreCompleto = "John Doe",
            Sueldo = 50000,
            NroDocumento = "12345678",
            IdDepartamento = 1,
            FechaContrato = DateTime.Now.AddDays(-10)
        },
        new EmpleadoResponseDTO
        {
            NombreCompleto = "Jane Smith",
            Sueldo = 60000,
            NroDocumento = "87654321",
            IdDepartamento = 2,
            FechaContrato = DateTime.Now.AddDays(-20)
        }
    };

        public async Task<EmpleadoResponseDTO> Create(EmpleadoCreateRequestDTO empleado)
        {
            var nuevoEmpleado = new EmpleadoResponseDTO
            {
                NombreCompleto = empleado.NombreCompleto,
                Sueldo = empleado.Sueldo,
                NroDocumento = empleado.NroDocumento,
                IdDepartamento = empleado.IdDepartamento,
                FechaContrato = empleado.FechaContrato
            };
            _empleados.Add(nuevoEmpleado);
            return await Task.FromResult(nuevoEmpleado);
        }

        public async Task<List<EmpleadoResponseDTO>> GetAll()
        {
            return await Task.FromResult(_empleados);
        }

        public async Task<List<EmpleadoResponseDTO>> GetByNombre(string nombre)
        {
            return await Task.FromResult(_empleados.Where(e => e.NombreCompleto.ToLower().Contains(nombre.ToLower())).ToList());
        }

        public async Task<EmpleadoResponseDTO> GetByNroDocumento(string nroDocumento)
        {
            var empleados = await GetAll();
            var empleado = empleados.FirstOrDefault(e => e.NroDocumento.Equals(nroDocumento));
            return empleado;
        }

        public async Task<bool> Delete(string nroDocumento)
        {
            var empleado = await GetByNroDocumento(nroDocumento);
            if (empleado != null)
            {
                _empleados.Remove(empleado);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<EmpleadoResponseDTO> Update(EmpleadoUpdateRequestDTO empleadoUpdate, string nroDocumento)
        {
            var empleadoExistente = _empleados.FirstOrDefault(e => e.NroDocumento == nroDocumento);
            if (empleadoExistente != null)
            {
                // Actualizar los campos del empleado existente

                // Verificar y actualizar el sueldo
                if (empleadoUpdate.Sueldo.HasValue)
                {
                    empleadoExistente.Sueldo = empleadoUpdate.Sueldo.Value;
                }

                if (!string.IsNullOrEmpty(empleadoUpdate.NombreCompleto))
                {
                    empleadoExistente.NombreCompleto = empleadoUpdate.NombreCompleto;
                }

                // Verificar y actualizar el departamento
                if (empleadoUpdate.IdDepartamento.HasValue)
                {
                    empleadoExistente.IdDepartamento = empleadoUpdate.IdDepartamento.Value;
                }

                // Verificar y actualizar la fecha de contrato
                if (empleadoUpdate.FechaContrato.HasValue)
                {
                    empleadoExistente.FechaContrato = empleadoUpdate.FechaContrato.Value;
                }

                return await Task.FromResult(empleadoExistente);
            }
            else
            {
                return await Task.FromResult<EmpleadoResponseDTO>(null);
            }
        }

    }
}
