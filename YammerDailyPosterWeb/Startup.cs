using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YammerDailyPosterWeb.Startup))]
namespace YammerDailyPosterWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
