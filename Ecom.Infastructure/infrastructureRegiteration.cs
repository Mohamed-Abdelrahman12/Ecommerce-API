using Ecom.Application.Interfaces;
using Ecom.Core.Services;
using Ecom.Domain.Entities;
using Ecom.Infastructure.Data;
using Ecom.Infastructure.Repositories;
using Ecom.infrastructure.Repositories;
using Ecom.infrastructure.Repositories.service;
using Ecom.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;

namespace Ecom.infrastructure
{
    public static class infrastructureRegiteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services,IConfiguration configuration) {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IOrderService, OrderService>();
            //register Email Sender
            services.AddScoped<IEmailService, EmailService>();
            //register token
            services.AddScoped<IGenerateToken, GenerateToken>();
            //register Payment Service
            services.AddScoped<IPaymentService, PaymentService>();

            services.AddScoped<IRatingRepository, RatingRepository>();
            services.AddScoped<IWishListRepository, WishListRepository>();
            //aplay redis connnection
            //services.AddScoped<IConnectionMultiplexer>(i =>
            //{
            //    var Config = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));

            //    return ConnectionMultiplexer.Connect(Config);
            //});
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = ConfigurationOptions.Parse(
                    configuration.GetConnectionString("redis")
                );

                options.AbortOnConnectFail = false;
                options.ConnectRetry = 5;
                options.ConnectTimeout = 5000;

                return ConnectionMultiplexer.Connect(options);
            });


            services.AddSingleton<IImageManagementService, ImageManagementService>();
            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));
            services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            });
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               
            }).AddCookie(o =>
            {
                o.Cookie.Name = "token";
                o.Cookie.SameSite = SameSiteMode.None;
                o.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
            }).AddJwtBearer(op =>
            {
                op.RequireHttpsMetadata = false;
                op.SaveToken = true;
                op.TokenValidationParameters = new()
                {
                    ValidateIssuerSigningKey =true,
                    IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes(configuration["Token:Secret"])),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["Token:Issure"],
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
                op.Events = new JwtBearerEvents()
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["token"];

                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }


    }
}
