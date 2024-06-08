using Application.Config;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Infrastructure.Context;
using Infrastructure.Mappers;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    /// <summary>
    /// This class is used to register services in the container.
    /// </summary>
    public static class ServiceCollection
    {
        /// <summary>
        /// Register services for the application.
        /// </summary>
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IDistributorService, DistributorService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IBookService, BookService>();
            return services;
        }

        /// <summary>
        /// Register services for the application.
        /// </summary>
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            return services;
        }

        /// <summary>
        /// Register services for the application.
        /// </summary>
        public static IServiceCollection AddBussinessDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = ConfigUtil.ConfigGlobal.ConnectionStrings.SystemDB;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string is null or empty");
            }
            services.AddDbContext<BookStreetContext>(o =>
            {
                o.UseMySQL(connectionString)
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine);
            });

            return services;
        }

        /// <summary>
        /// Register automapper for the application.
        /// </summary>
        public static IServiceCollection AddExternalService(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
