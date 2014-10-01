using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookLists.Startup))]
namespace BookLists
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
