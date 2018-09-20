using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Security.Principal;

namespace PruebasWeb
{
    public class BasicAuthenticationAttribute: AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            else
            {
                string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;
                string decodedAuthorizationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

                string[] usernamePasswordArray = decodedAuthorizationToken.Split(':');

                string username = usernamePasswordArray[0];
                string password = usernamePasswordArray[1];

                if(Seguridad.Login(username, password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

                base.OnAuthorization(actionContext);

            }
        }
    }
}