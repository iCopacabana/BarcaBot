using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Barcabot.Common;
using Hangfire;
using Hangfire.Dashboard;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Barcabot.Database;
using Barcabot.Web;
using Barcabot.HangfireService.Services;

namespace Barcabot.HangfireService
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
            services.AddHangfire(x => x.UsePostgreSqlStorage(ConnHelper.GetConnectionString(YamlConfiguration.Config.Postgres.DatabaseNames.Hangfire)));
            services.AddHangfireServer();
            services.AddSingleton<HttpClient>();
            services.AddTransient<DataRetrievalService>();
            services.AddTransient<FootballDataApiRetrievalService>();
            services.AddTransient<ApiFootballRetrievalService>();
            services.AddTransient<PlayerRetriever>();
            services.AddTransient<FootballDataRetriever>();
            services.AddTransient<FootballDataUpdaterService>();
            services.AddTransient<PlayerUpdaterService>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = Enumerable.Empty<IDashboardAuthorizationFilter>()
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}