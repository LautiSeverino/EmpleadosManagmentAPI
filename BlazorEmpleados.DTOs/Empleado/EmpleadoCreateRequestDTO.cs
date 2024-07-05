using BlazorEmpleados.DTOs.Departamento;
using System.ComponentModel.DataAnnotations;

namespace BlazorEmpleados.DTOs.Empleado
{
    public class EmpleadoCreateRequestDTO
    {

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string NombreCompleto { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int IdDepartamento { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} es requerido.")]
        public int Sueldo { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string NroDocumento { get; set; } = null!;

        public DateTime FechaContrato { get; set; }

        //public DepartamentoResponseDTO? Departamento { get; set; }

    }
}
