using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RegistroIdentity.Controllers
{
    [Authorize]
    public class EmpleadosController : ApiController
    {
        public IEnumerable<Empleado> GetEmpleados()
        {
            using(PruebasEntityEntities DB = new PruebasEntityEntities())
            {
                return DB.Empleado.ToList();
            }
        }
    }
}
