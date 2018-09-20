using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using WebApiContrib.Formatting.Jsonp;
using System.Web.Http.Cors;

namespace PruebasWeb
{
    public static class WebApiConfig
    {
        //public class CustomJsonFormatter : JsonMediaTypeFormatter
        //{
        //    public CustomJsonFormatter()
        //    {
        //        this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        //    }

        //    public override void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
        //    {
        //        base.SetDefaultContentHeaders(type, headers, mediaType);
        //        headers.ContentType = new MediaTypeHeaderValue("application/json");
        //    }
        //}
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            //config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            //config.Formatters.Remove(config.Formatters.JsonFormatter);

            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new System.Net.Http.Headers.MediaTypeHeaderValue("text/html"));

            //config.Formatters.Add(new CustomJsonFormatter());

            //var jsonpFormatter = new JsonpMediaTypeFormatter(config.Formatters.JsonFormatter);
            //config.Formatters.Insert(0, jsonpFormatter);

            //EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:55540","Accept,Content-type","*");
            config.EnableCors();

            //config.Filters.Add(new RequireHttpsAttribute());
            config.Filters.Add(new BasicAuthenticationAttribute());
        }
    }
}
