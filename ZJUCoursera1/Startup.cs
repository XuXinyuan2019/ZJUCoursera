using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZJUCoursera1.Startup))]
namespace ZJUCoursera1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
