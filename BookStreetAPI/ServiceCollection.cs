using Application.Config;
using BookStreetAPI.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BookStreetAPI
{
    public static class ServiceCollection
    {
        private static CenterConfig CenterConfig;
        /// <summary>
        /// Lấy toàn bộ cấu hình từ appsettings đẩy vào hàm CenterConfig cho dễ dùng
        /// </summary>ss
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppsettings(this IServiceCollection services, IConfiguration configuration)
        {
            // IoC CenterConfig
            services.Configure<CenterConfig>(configuration);
            var centerConfig = new CenterConfig();
            new ConfigureFromConfigurationOptions<CenterConfig>(configuration).Configure(centerConfig);
            services.AddSingleton(centerConfig);
            CenterConfig = centerConfig;
            ConfigUtil.SetConfigGlobal(centerConfig);
            return services;
        }

        /// <summary>
        /// Thêm các dịch vụ cần thiết cho WebAPI
        /// </summary>
        public static IServiceCollection AddWebAPI(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<JwtMiddleware>();
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();
            services.AddLogging();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddCors();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        /// <summary>
        /// Thêm cấu hình cho JWT
        /// </summary>
        public static IServiceCollection AddJwtConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = ConfigUtil.ConfigGlobal.JWTConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = ConfigUtil.ConfigGlobal.JWTConfig.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigUtil.ConfigGlobal.JWTConfig.Secret))
                };
            });

            // Add policy authorization
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy(RMSRole.WAITER, p => p.RequireClaim(ClaimTypes.Role, RMSRole.WAITER));
            //    options.AddPolicy(RMSRole.CHEF, p => p.RequireClaim(ClaimTypes.Role, RMSRole.CHEF));
            //    options.AddPolicy(RMSRole.MANAGER, p => p.RequireClaim(ClaimTypes.Role, RMSRole.MANAGER));
            //    options.AddPolicy(RMSRole.EMPLOYEE, p => p.RequireClaim(ClaimTypes.Role, RMSRole.WAITER, RMSRole.CHEF, RMSRole.CASHIER, RMSRole.MANAGER));
            //});
            return services;
        }
    }
}
