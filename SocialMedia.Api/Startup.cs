using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastrucuture.Data;
using SocialMedia.Infrastrucuture.Data.Configuration;
using SocialMedia.Infrastrucuture.Data.Configuration.Abstract;
using SocialMedia.Infrastrucuture.Filters;
using SocialMedia.Infrastrucuture.Interfaces;
using SocialMedia.Infrastrucuture.Repositories;
using SocialMedia.Infrastrucuture.Services;
using System;

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
            // configure AutoMapper to find automapper profiles
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

            // DEPENDENCIAS

            // database mappers
            services.AddTransient<IUserConfiguration, UserConfiguration>();
            services.AddTransient<ICommentConfiguration, CommentConfiguration>();
            services.AddTransient<IPostConfiguration, PostConfiguration>();

            // register config

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));

            // db context
            services.AddDbContext<SocialMediaContext>();

            /* repositories and services */

            // post
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IPostRepository, PostRepository>();

            // generic
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            // uri service
            services.AddSingleton<IUriService>(provider => 
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
