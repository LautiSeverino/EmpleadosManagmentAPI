﻿using BlazorEmpleados.DAL.Data;
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
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(EmpleadosDbContext context) : base(context)
        {
            
        }
        public async Task<User> GetUser(string userName, string password)
        {
            var usuario = await _context.Users.Where(x => x.UserName == userName && x.UserPassword == password).FirstOrDefaultAsync();
            return usuario;
        }
    }
}
