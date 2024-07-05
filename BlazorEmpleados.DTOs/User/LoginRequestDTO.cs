using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DTOs.User
{
    public class LoginRequestDTO
    {
        public string UserName { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
    }
}
