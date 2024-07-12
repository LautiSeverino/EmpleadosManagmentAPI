using BlazorEmpleados.BLL.Interface;
using BlazorEmpleados.DAL.Data.Migrations;
using BlazorEmpleados.DTOs.Departamento;
using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.Departamento
{
    public class StubServiceDepartamento : IDepartamentoService
    {
        private List<DepartamentoResponseDTO> _departamentos = new List<DepartamentoResponseDTO>
        {
            new DepartamentoResponseDTO
            {
                IdDepartamento = 1,
                Nombre = "Recursos Humanos"
            },new DepartamentoResponseDTO
            {
                IdDepartamento = 2,
                Nombre = "Desarrollo"
            },new DepartamentoResponseDTO
            {
                IdDepartamento = 3,
                Nombre = "Logistica"
            },
        };

        public async Task<DepartamentoResponseDTO> Create(DepartamentoCreateDTO departamento)
        {
            var nuevoDepartamento = new DepartamentoResponseDTO
            {
                IdDepartamento = 4,
                Nombre = departamento.Nombre,
            };
            _departamentos.Add(nuevoDepartamento);
            return await Task.FromResult(nuevoDepartamento);
        }

        public async Task<bool> Delete(int id)
        {
            var departamento = _departamentos.FirstOrDefault(d => d.IdDepartamento == id);
            if (departamento != null)
            {
                _departamentos.Remove(departamento);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }

        public async Task<List<DepartamentoResponseDTO>> GetAll()
        {
            return await Task.FromResult(_departamentos);
        }
    }
}
