using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastrucuture.Data;
using SocialMedia.Infrastrucuture.Data.Configuration;
using SocialMedia.Infrastrucuture.Data.Configuration.Abstract;
using SocialMedia.Infrastrucuture.Repositories;
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
                });

            // DEPENDENCIAS

            // mappers
            services.AddTransient<IUserConfiguration, UserConfiguration>();
            services.AddTransient<ICommentConfiguration, CommentConfiguration>();
            services.AddTransient<IPostConfiguration, PostConfiguration>();
            // db context
            services.AddDbContext<SocialMediaContext>();
            // repositories
            services.AddTransient<IPostRepository, PostRepository>();
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
