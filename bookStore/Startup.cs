using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(bookStore.Startup))]
namespace bookStore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
