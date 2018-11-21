using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BooksProject.Startup))]
namespace BooksProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
