using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;

namespace PruebasWeb
{
    public class Seguridad
    {
        public static bool Login(string username, string password)
        {
            using(PruebasEntityEntities DB = new PruebasEntityEntities())
            {
                return DB.Usuario.Any(u => u.Username.Equals(username,StringComparison.OrdinalIgnoreCase) && u.Password.Equals(password));
            }

        }
    }
}