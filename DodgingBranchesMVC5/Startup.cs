using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DodgingBranchesMVC5.Startup))]
namespace DodgingBranchesMVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
