using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poof.Core.Future;
using Poof.Core.Model.Data;
using Poof.Core.Model.Future;
using Poof.Core.Snaps.Quest;
using Poof.DB.Data;
using Poof.DB.Models;
using Poof.Snaps;
using Poof.Web.Server.Data;
using Poof.WebApp.Server.UserStatus;
using Pulse;

namespace Poof.WebApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddScoped<IDataBuilding, DbBuilding>();
            AddPulse(services);
            services.AddScoped<IFuture, FutureOf>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseIdentityServer();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

            InitializeFuture(app);
        }

        private void AddPulse(IServiceCollection services)
        {
            var pulse = new SyncPulse();
            var status = new SimpleStatus();
            pulse.Connect(new SnsStatusChanged(status));

            services.AddSingleton<IPulse>(pulse);
            services.AddSingleton<IIdentityStatus>(status);
        }

        private void InitializeFuture(IApplicationBuilder app)
        {
            var scope = app.ApplicationServices.CreateScope();
            var mem = scope.ServiceProvider.GetService<IDataBuilding>();
            var future = scope.ServiceProvider.GetService<IFuture>();
            future.RunAsync();

            new InitializesQuests(mem, future).Convert(
                new EmptyDemand()
            );
            new InitializesExpiries(mem, future).Convert(
                new EmptyDemand()
            );
        }
    }
}
