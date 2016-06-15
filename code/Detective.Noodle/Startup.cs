using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Detective.Noodle.Startup))]
namespace Detective.Noodle
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
