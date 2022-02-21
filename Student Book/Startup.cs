using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Student_Book.Startup))]
namespace Student_Book
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
