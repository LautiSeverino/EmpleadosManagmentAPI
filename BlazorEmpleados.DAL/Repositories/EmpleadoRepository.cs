using BlazorEmpleados.DAL.Data;
using BlazorEmpleados.DAL.Repositories.Interface;
using BlazorEmpleados.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DAL.Repositories
{
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(EmpleadosDbContext context) : base(context)
        {
        }

        public async Task<List<Empleado>> GetAllWithDepartamento()
        {
            return await _context.Empleados.Include(x => x.IdDepartamentoNavigation).ToListAsync();
        }

        public async Task<List<Empleado>> GetByNombre(string nombre)
        {
            var result = await _context.Empleados
                .Include(x => x.IdDepartamentoNavigation)
                .Where(x => x.NombreCompleto.ToLower().Contains(nombre.ToLower()))
                .ToListAsync();
            return result;
        }
        public async Task<Empleado> GetByNroDocumento(string nroDocumento)
        {
            var result = await _context.Empleados
                .Include(x => x.IdDepartamentoNavigation)
                .Where(x => x.NroDocumento.Equals(nroDocumento)).FirstOrDefaultAsync();
            return result;
        }
    }
}
