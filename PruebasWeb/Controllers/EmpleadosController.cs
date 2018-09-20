using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccess;
using System.Web.Http.Cors;
using System.Threading;

namespace PruebasWeb.Controllers
{
    [EnableCorsAttribute("http://localhost:55540,https://localhost:44301", "Accept,Content-type", "*")]
    public class EmpleadosController : ApiController
    {
        //[RequireHttps]
        [BasicAuthentication]
        public HttpResponseMessage GetEmpleados(string puesto="Todos")
        {
            string username = Thread.CurrentPrincipal.Identity.Name;

            using (PruebasEntityEntities DB = new PruebasEntityEntities())
            {
                switch(puesto.ToLower()){
                    case "todos":
                        return Request.CreateResponse(HttpStatusCode.OK, DB.Empleado.ToList());
                    case "desarrollador":
                        return Request.CreateResponse(HttpStatusCode.OK, DB.Empleado.Where(e => e.Puesto.ToLower() == puesto).ToList());
                    case "tester":
                        return Request.CreateResponse(HttpStatusCode.OK, DB.Empleado.Where(e => e.Puesto.ToLower() == puesto).ToList());
                    case "analista":
                        return Request.CreateResponse(HttpStatusCode.OK, DB.Empleado.Where(e => e.Puesto.ToLower() == puesto).ToList());
                    default:
                        return Request.CreateResponse(HttpStatusCode.BadRequest, string.Format("{0} no es un nombre de puesto válido",puesto));
                }
            }
        }

        [DisableCors]
        public HttpResponseMessage GetEmpleados(int id)
        {
            using (PruebasEntityEntities DB = new PruebasEntityEntities())
            {
                var entity = DB.Empleado.FirstOrDefault(e => e.IdEmpleado == id);

                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,entity);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Empleado con ID "+id+" No encontrado");
                }
            }
        }

        public HttpResponseMessage PostEmpleado([FromBody]Empleado empleado)
        {
            using(PruebasEntityEntities DB = new PruebasEntityEntities())
            {
                try
                {
                    DB.Empleado.Add(empleado);
                    DB.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, empleado);
                    message.Headers.Location = new Uri(Request.RequestUri + empleado.IdEmpleado.ToString());
                    return message;
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
                }
            }
        }

        public HttpResponseMessage DeleteEmpleado(int id)
        {
            using(PruebasEntityEntities DB = new PruebasEntityEntities())
            {
                try
                {
                    var entity = DB.Empleado.FirstOrDefault(e => e.IdEmpleado == id);
                    if (entity != null)
                    {
                        DB.Empleado.Remove(entity);
                        DB.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound, "Empleado con ID " + id.ToString() + " no encontrado para borrar");
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
                }                
            }
        }

        public HttpResponseMessage PutEmpleado(int id, [FromBody]Empleado empleado)
        {
            using(PruebasEntityEntities DB = new PruebasEntityEntities())
            {
                try
                {
                    var entity = DB.Empleado.FirstOrDefault(e => e.IdEmpleado == id);

                    if (entity != null)
                    {
                        entity.Nombre = empleado.Nombre;
                        entity.ApellidoPaterno = empleado.ApellidoPaterno;
                        entity.ApellidoMaterno = empleado.ApellidoMaterno;
                        entity.Telefono = empleado.Telefono;

                        DB.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK,entity);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.NotFound,"Empleado con ID "+id.ToString()+" no encontrado");
                    }
                }
                catch(Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ex);
                }
            }
        }
    }
}
