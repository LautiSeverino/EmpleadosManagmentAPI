using BlazorEmpleados.DTOs.Departamento;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DTOs.Empleado
{
    public class EmpleadoResponseDTO
    {
        public string NombreCompleto { get; set; } = null!;

        public int IdDepartamento { get; set; }

        public int Sueldo { get; set; }
        public string NroDocumento { get; set; } = null!;
        public string FechaContrato { get; set; }

        public DepartamentoResponseDTO? IdDepartamentoNavigation { get; set; }
    }
}
