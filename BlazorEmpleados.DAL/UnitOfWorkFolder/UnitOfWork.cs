using BlazorEmpleados.DAL.Data;
using BlazorEmpleados.DAL.Repositories.Interface;
using BlazorEmpleados.DAL.UnitOfWork.UnitOfWorkFolder;

namespace BlazorEmpleados.DAL.UnitOfWorkFolder
{
    public class UnitOfWork : IUnitOfWork
    {
        public IEmpleadoRepository EmpleadoRepository {  get;}
        public IDepartamentoRepository DepartamentoRepository { get;}

        public IUserRepository UserRepository { get;}

        private readonly EmpleadosDbContext _context;
        public UnitOfWork(EmpleadosDbContext context, IEmpleadoRepository empleadoRepository, IDepartamentoRepository departamentoRepository, IUserRepository userRepository)
        {
            _context = context;
            EmpleadoRepository = empleadoRepository;
            DepartamentoRepository = departamentoRepository;
            UserRepository = userRepository;

        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<int> Save()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
