using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Poof.Web.Server.Areas.Identity.IdentityHostingStartup))]
namespace Poof.Web.Server.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}