using GreenLebanon.Taxi.Application.Services;
using GreenLebanon.Taxi.ApplicationCore.Entities;
using GreenLebanon.Taxi.ApplicationCore.Repositories;
using GreenLebanon.Taxi.Infrastructure.Data;
using GreenLebanon.Taxi.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace GreenLebanon.Taxi.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddScoped<AppDbContext>();

            builder.Services.AddScoped<IClientRepository, ClientRepository>();
            builder.Services.AddScoped<ClientService>();
            builder.Services.AddScoped<AccountService>();

            builder.Services.AddScoped<IJwtFactory, JwtFactory>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle


            builder.Configuration.AddJsonFile("appsettings.json", true, true);

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Services.AddSwaggerGen();
            builder.Services.AddEndpointsApiExplorer();

            var secretKey = builder.Configuration.GetSection("JwtIssuerOptions:SecretKey").Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

            builder.Services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = builder.Configuration.GetSection("JwtIssuerOptions:Issuer").Value;
                options.Audience = builder.Configuration.GetSection("JwtIssuerOptions:Audience").Value;
                options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration.GetSection("JwtIssuerOptions:Issuer").Value,

                ValidateAudience = true,
                ValidAudience = builder.Configuration.GetSection("JwtIssuerOptions:Audience").Value,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.ClaimsIssuer = builder.Configuration.GetSection("JwtIssuerOptions:Issuer").Value;
                    configureOptions.TokenValidationParameters = tokenValidationParameters;
                    configureOptions.SaveToken = true;
                });

            builder.Services.AddAuthorizationBuilder()
                .AddPolicy("ApiUser", policy => policy.RequireClaim("rol", "api_access"));

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

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
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();
            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseRouting();

            app.Run();
        }
    }
}
