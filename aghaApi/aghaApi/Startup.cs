using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(aghaApi.Startup))]
namespace aghaApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
