using GreenLebanon.Taxi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GreenLebanon.Taxi.API
{
    public class Program {
        public static void Main( string[] args ) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Configuration.AddJsonFile("appsettings.json", true, true);
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() ) {
                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    options.RoutePrefix = string.Empty;

                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
