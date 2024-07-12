using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DTOs.User
{
    public class UserCreateRequestDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MinLength(4)]
        public string UserName { get; set; } = null!;

        [MinLength(10)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserEmail { get; set; } = null!;

        [MinLength(4)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserPhone { get; set; } = null!;

        [MinLength(4)]
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserPassword { get; set; } = null!;
    }
}
