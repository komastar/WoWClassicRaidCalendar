using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WCRC.Startup))]
namespace WCRC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
