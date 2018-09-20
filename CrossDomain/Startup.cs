using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrossDomain.Startup))]
namespace CrossDomain
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
