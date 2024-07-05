using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DTOs.Departamento
{
    public class DepartamentoCreateDTO
    {
        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; } = null!;
    }
}
