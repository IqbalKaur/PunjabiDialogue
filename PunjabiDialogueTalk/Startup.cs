using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PunjabiDialogueTalk.Startup))]
namespace PunjabiDialogueTalk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
