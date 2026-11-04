using DemoShoppe.Infrastructure.DependencyInjection.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace DemoShoppe.API.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                JwtOption jwtOption = new JwtOption();
                configuration.GetSection(nameof(jwtOption)).Bind(jwtOption);
                var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
                //var secretKey = Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!)
                //?? throw new InvalidOperationException(
                //    "JWT SecretKey is missing.");
                o.SaveToken = true;

                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = jwtOption.Issuer,
                    ValidAudience = jwtOption.Audience,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero,
                    NameClaimType = ClaimTypes.Name,
                    RoleClaimType = ClaimTypes.Role,
                };
                o.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if(context.Exception.GetType() == typeof(SecurityTokenExpiredException)) 
                        {
                            context.Response.Headers.Add("Is-Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
        
    }
}
