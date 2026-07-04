using Microsoft.IdentityModel.JsonWebTokens;
using MoneyGo.Application.Common.Interfaces;
using Scalar.AspNetCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MoneyGo.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
            {
                // Add services to the container.
                builder.Services.AddAppDI(builder.Configuration);

                builder.Services.Configure<JwtOptions>(
                    builder.Configuration.GetSection("Jwt"));

                var jwtSettings = builder.Configuration
                    .GetSection("Jwt")
                    .Get<JwtOptions>()!;

                builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new()
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            
                            ValidIssuer = jwtSettings.Issuer,
                            ValidAudience = jwtSettings.Audience,
                            
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.UTF8.GetBytes(jwtSettings.Key))
                        };
                    });

                builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

                builder.Services.AddHttpContextAccessor();
                builder.Services.AddScoped<ICurrentUserService, CurrentUserSerivce>();

                builder.Services.AddControllers()
                    .AddJsonOptions(options =>
                    {
                        var enumConverter = new JsonStringEnumConverter(JsonNamingPolicy.CamelCase);
                        options.JsonSerializerOptions.Converters.Add(enumConverter);

                        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    });
                // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
                builder.Services.AddOpenApi();
            }
            

            var app = builder.Build();
            {
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                    app.MapScalarApiReference();
                }

                app.UseHttpsRedirection();

                app.UseAuthentication();
                app.UseAuthorization();


                app.MapControllers();

                app.Run();
            }
            
        }
    }
}
