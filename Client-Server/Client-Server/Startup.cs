using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Client_Server.Startup))]
namespace Client_Server
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
