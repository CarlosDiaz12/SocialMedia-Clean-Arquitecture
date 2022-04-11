using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Infrastrucuture;
using SocialMedia.Infrastrucuture.Filters;
using System;
using System.IO;
using System.Reflection;

namespace SocialMedia.Api
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
            // configure json reference loop
            services.AddControllers()
                .AddNewtonsoftJson(x => {
                    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    // x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(opts => {
                    // configurar opciones de API Attribute / remover validacion de modelo antes de entrar al action
                    // opts.SuppressModelStateInvalidFilter = true;
                });

            // register config

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.AddInfrastructureServices();

            // swaggger docs
            services.AddSwaggerGen(opts =>
            {
                opts.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Social Media API", 
                    Version = "v1", 
                    Contact = new OpenApiContact { Name = "Carlos Diaz"} 
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                opts.IncludeXmlComments(xmlPath);
            });

            // Configurar middleware global
            services.AddMvcCore(opts =>
            {
                opts.Filters.Add(typeof(ValidationFilter));
                opts.Filters.Add(typeof(GlobalExceptionFilter));

            })
                .AddFluentValidation( opts => {
                    // REGISTRAR LOS ASSEMBLIES DONDE SE ENCUENTRAN LOS VALIDATORS de FLUENT VALIDATION
                    opts.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options => 
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Social Media API V1");
                options.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
