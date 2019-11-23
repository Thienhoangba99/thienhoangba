using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bai5_Anh.Startup))]
namespace bai5_Anh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
