using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MVC_Start.DataAccess;

namespace MVC_Start
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
            // Added from MVC template

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration["Data:SchoolProgram:ConnectionString"]));
            services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    // the version below came with the empty template
    //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    //{
    //  if (env.IsDevelopment())
    //  {
    //    app.UseDeveloperExceptionPage();
    //  }

    //  app.Run(async (context) =>
    //  {
    //    await context.Response.WriteAsync("Hello World!");
    //  });
    //}

    // this is the version from the MVC template. Compare to above for changes
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {

            //This ensures that the database and tables are created as per the Models.
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }

            if (env.IsDevelopment())
      {
        app.UseBrowserLink();
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
      }

      app.UseStaticFiles();

      app.UseMvc(routes =>
      {
        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
      });




    }
  }
}
