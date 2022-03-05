using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HackathonApp.Startup))]
namespace HackathonApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
