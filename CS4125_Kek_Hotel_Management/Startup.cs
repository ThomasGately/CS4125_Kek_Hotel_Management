using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CS4125_Kek_Hotel_Management.Startup))]
namespace CS4125_Kek_Hotel_Management
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
