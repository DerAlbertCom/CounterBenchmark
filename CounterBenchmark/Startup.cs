using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CounterBenchmark.Startup))]
namespace CounterBenchmark
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
