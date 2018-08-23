using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vidly.Startup))]
namespace vidly
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
