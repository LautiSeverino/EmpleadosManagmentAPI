using BlazorEmpleados.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DAL.Data
{
    public class EmpleadosDbContext : DbContext
    {
        public EmpleadosDbContext()
        {
            
        }
        public EmpleadosDbContext(DbContextOptions<EmpleadosDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        public virtual DbSet<Empleado> Empleados { get; set;}
        public virtual DbSet<Departamento> Departamentos { get; set;}    
        public virtual DbSet<User> Users { get; set;}
    }
}
