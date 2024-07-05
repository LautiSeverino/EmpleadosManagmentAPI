using BlazorEmpleados.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEmpleados.DAL.Repositories.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetUser(string userName, string password);

    }
}
