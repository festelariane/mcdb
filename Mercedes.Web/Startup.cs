using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mercedes.Web.Startup))]
namespace Mercedes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
