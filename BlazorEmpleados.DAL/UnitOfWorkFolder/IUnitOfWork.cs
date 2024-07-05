using BlazorEmpleados.DAL.Repositories;
using BlazorEmpleados.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DAL.UnitOfWork.UnitOfWorkFolder
{
    public interface IUnitOfWork : IDisposable
    {
        IEmpleadoRepository EmpleadoRepository { get; }
        IDepartamentoRepository DepartamentoRepository { get; }
        IUserRepository UserRepository { get; }
        Task<int> Save();
    }
}
