using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignmentONE.Data;
using Microsoft.EntityFrameworkCore;
using assignmentONE.Helper;
using Microsoft.AspNetCore.HttpOverrides;

namespace assignmentONE
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
            services.AddDbContext<WebAPIDBContext>(options => options.UseSqlite(Configuration.GetConnectionString("WebAPIConnection")));
            services.AddControllers();
            services.AddScoped<IStaffAPIRepo, DBStaffAPIRepo>();
            services.AddScoped<IProductAPIRepo, DBProductAPIRepo>();
            services.AddScoped<ICommentsAPIRepo, DBCommentsAPIRepo>();
            services.AddMvc(options => options.OutputFormatters.Add(new VCardOutputFormatter()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            
            }
           
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
