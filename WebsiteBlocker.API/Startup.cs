using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebsiteBlocker.Domain.Facades;
using WebsiteBlocker.Domain.Factories;
using WebsiteBlocker.Domain.Interfaces.Facades;
using WebsiteBlocker.Domain.Interfaces.Factories;
using WebsiteBlocker.Domain.Interfaces.Services;
using WebsiteBlocker.Domain.Services;

namespace WebsiteBlocker.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IWebsiteBlockerFacade, WebsiteBlockerFacade>();
            services.AddScoped<IWebsiteBlockerService, WebsiteBlockerService>();
            services.AddScoped<IWebsiteBlockerCheckFactory, WebsiteBlockerCheckFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
