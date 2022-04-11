using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infrastrucuture.Data;
using SocialMedia.Infrastrucuture.Data.Configuration;
using SocialMedia.Infrastrucuture.Data.Configuration.Abstract;
using SocialMedia.Infrastrucuture.Interfaces;
using SocialMedia.Infrastrucuture.Repositories;
using SocialMedia.Infrastrucuture.Services;
using System;

namespace SocialMedia.Infrastrucuture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // DEPENDENCIAS

            // configure AutoMapper to find automapper profiles
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // database mappers
            services.AddTransient<IUserConfiguration, UserConfiguration>();
            services.AddTransient<ICommentConfiguration, CommentConfiguration>();
            services.AddTransient<IPostConfiguration, PostConfiguration>();


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
            return services;
        }
    }
}
