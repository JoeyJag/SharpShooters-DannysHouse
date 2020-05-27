using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Sharpshooter.Startup))]
namespace Sharpshooter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
