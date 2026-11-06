using Ecom.API.MiddleWare;
using Ecom.Application.Extensions;
using Ecom.infrastructure;
namespace Ecom.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(op =>
            {
                op.AddPolicy("CORSPolicy", builder => 
                {
                    builder.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                    //builder.AllowAnyOrigin()  
               //.AllowAnyHeader()
               //.AllowAnyMethod()
               //.AllowCredentials();

                });

            });

            // Add services to the container.
            builder.Services.AddMemoryCache();
            builder.Services.AddApplication();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.infrastructureConfiguration(builder.Configuration);
            builder.Services.AddAutoMapper(map=>map.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

            var app = builder.Build();
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
           
            app.UseMiddleware<EceptionsMiddleware>();
            app.UseCors("CORSPolicy");

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();


            app.MapControllers();

            app.Run();
        }
    }
}
