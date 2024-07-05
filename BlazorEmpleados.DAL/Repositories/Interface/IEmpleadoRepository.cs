using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DAL.Repositories.Interface
{
    public interface IEmpleadoRepository : IRepository<Empleado>
    {
        Task<List<Empleado>> GetByNombre(string nombre);
        Task<Empleado> GetByNroDocumento(string nroDocumento);
        Task<List<Empleado>> GetAllWithDepartamento();
    }
}
