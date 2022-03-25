using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LogisticManagement.Startup))]
namespace LogisticManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
