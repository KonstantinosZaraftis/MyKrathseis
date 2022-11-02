using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Krathseis2.Startup))]
namespace Krathseis2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
