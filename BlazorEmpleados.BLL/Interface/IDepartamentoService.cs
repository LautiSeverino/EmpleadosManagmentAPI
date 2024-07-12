using BlazorEmpleados.DTOs.Departamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.BLL.Interface
{
    public interface IDepartamentoService
    {
        Task<List<DepartamentoResponseDTO>> GetAll();
        Task<DepartamentoResponseDTO> Create(DepartamentoCreateDTO departamento);
    }
}
