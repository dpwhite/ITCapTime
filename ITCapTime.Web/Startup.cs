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
using ITCapTime.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ITCapTime.API.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using ITCapTime.API;

namespace ITCapTime
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            this.env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var projectAssembly = typeof(ProjectsController).GetTypeInfo().Assembly;
            var userAssembly = typeof(AuthController).GetTypeInfo().Assembly;

            services.AddControllersWithViews(config => { })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = actionContext =>
                    {
                        return new BadRequestObjectResult(actionContext.ModelState.Values.SelectMany(v => v.Errors).Select(m => m.ErrorMessage).ToList());
                    };
                })
                .AddApplicationPart(userAssembly)
                .AddApplicationPart(projectAssembly);
                

            //services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ITCapTime", Version = "v1" });
            //});

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<ITCapTimeContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentityCore<ApplicationUser>()
                .AddEntityFrameworkStores<ITCapTimeContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddAuthentication(o => { o.DefaultScheme = IdentityConstants.ApplicationScheme; })
                .AddCookie(IdentityConstants.ApplicationScheme, options =>
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };

                    options.Events.OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    };
                });
            
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ITCapTime v1"));
            }            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSpaStaticFiles();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                if (!env.IsDevelopment()) return;
                //spa.UseProxyToSpaDevelopmentServer("http://localhost:4201");
                spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
            });

            ConfigurationMapper.ConfigureMapper();
        }
    }
}
