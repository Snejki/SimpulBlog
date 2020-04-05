using Autofac;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpulBlog.API.Extensions;
using SimpulBlog.API.Filters;
using SimpulBlog.Core.Modules;
using SimpulBlog.Domain;
using SimpulBlog.Domain.Modules;
using SimpulBlog.Infrastructure.Modules;
using SimpulBlog.Infrastructure.Services.Abstract;

namespace SimpulBlog.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IWebHostEnvironment env)
        {
            // In ASP.NET Core 3.0 `env` will be an IWebHostingEnvironment, not IHostingEnvironment.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(opts => {
                opts.Filters.Add(typeof(ModelStateValidator));
                //opts.Filters.Add(new Microsoft.AspNetCore.Mvc.ProducesResponseTypeAttribute());
                opts.Filters.Add(new ProducesResponseTypeAttribute(500));
            }).AddFluentValidation(fv
                => fv.RegisterValidatorsFromAssemblyContaining<IService>());

            services.AddDbContext<BlogContext>(options
                => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMediatR(typeof(IService));
            services.AddAutoMapper(typeof(IService));
            services.AddJwtAuthentication(Configuration);
            services.AddSwagger();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac, like:
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new SettingsModule(Configuration));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseGlobalExceptionHandler();
            app.UseSwaggerExt();
            app.UseSwaggerUI();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
