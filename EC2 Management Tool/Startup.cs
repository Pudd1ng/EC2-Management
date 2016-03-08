using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EC2_Management_Tool.Startup))]
namespace EC2_Management_Tool
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
