using Datos;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Logica
{
    public class UserService
    {
        private readonly PulsacionesContext _context;
        public UserService(PulsacionesContext context)
        {
            _context = context;
        }

        public User Validate(string userName, string password) 
        {
            return _context.Users.FirstOrDefault(t => t.UserName == userName && t.Password == password && t.Estado == "AC");
        }
    }
}
