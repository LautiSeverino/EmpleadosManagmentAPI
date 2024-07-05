using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEmpleados.Models
{
    public class Empleado
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdEmpleado { get; set; }
        public string NombreCompleto { get; set; } = null!;
        [ForeignKey(nameof(IdDepartamentoNavigation))]
        public int IdDepartamento { get; set;}
        public string NroDocumento { get; set; } = null!;
        public int Sueldo { get; set; }
        public DateTime FechaContrato { get; set; }
        public Departamento IdDepartamentoNavigation { get; set; } = null!;
    }
}
