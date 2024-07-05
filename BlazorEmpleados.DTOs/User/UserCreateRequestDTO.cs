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
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserEmail { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserPhone { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public string UserPassword { get; set; } = null!;
    }
}
