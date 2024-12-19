using GreenLebanon.Taxi.API.Data;
using GreenLebanon.Taxi.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace GreenLebanon.Taxi.API
{
    public class Program {
        public static void Main( string[] args ) {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
 
            builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();
            builder.Configuration.AddJsonFile("appsettings.json", true, true);
            
            builder.Services.AddDbContext<AppIdentityDbContext>(options => 
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

             builder.Services
               .AddIdentityApiEndpoints<IdentityUser>()
               .AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddCors(options =>
                 options.AddDefaultPolicy(builder =>
                        builder
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithOrigins("https://localhost:7241")
                       .AllowCredentials()
           )
          );



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if ( app.Environment.IsDevelopment() ) {
            //    app.UseSwagger();
            //    app.UseSwaggerUI(options => {
            //        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            //        options.RoutePrefix = string.Empty;

            //    });
            //}
            if ( app.Environment.IsDevelopment() ) {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapIdentityApi<IdentityUser>();
            app.MapControllers();
            app.UseRouting();
           
            app.Run();
        }
    }
}
