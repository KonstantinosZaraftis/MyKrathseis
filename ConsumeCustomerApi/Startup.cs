using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ConsumeCustomerApi.Startup))]
namespace ConsumeCustomerApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
