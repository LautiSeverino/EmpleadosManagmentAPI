using BlazorEmpleados.DAL.Data;
using BlazorEmpleados.DAL.Repositories.Interface;
using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DAL.Repositories
{
    public class DepartamentoRepository : Repository<Departamento>, IDepartamentoRepository
    {
        public DepartamentoRepository(EmpleadosDbContext context) : base(context)
        {
        }
    }
}
