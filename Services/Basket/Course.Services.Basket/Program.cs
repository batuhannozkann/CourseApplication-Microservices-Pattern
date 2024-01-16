
using System.IdentityModel.Tokens.Jwt;
using Course.Services.Basket.Dtos;
using Course.Services.Basket.Services;
using Course.Services.Basket.Services.Abstract;
using Course.Services.Basket.Settings;
using Course.SharedLibrary.Services;
using Course.SharedLibrary.Services.Abstract;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Course.Services.Basket.Filters;

namespace Course.Services.Basket
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var requiredAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

            // Add services to the container.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<BasketItemDtoValidator>();
            builder.Services.AddControllers(options =>
            {
                options.Filters.Add(new AuthorizeFilter(requiredAuthorizePolicy));
                options.Filters.Add<ValidationFilter>();
            });
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.Authority = builder.Configuration["IdentityServerUrl"];
                x.Audience = "resource_basket";
                x.RequireHttpsMetadata = false;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            builder.Services.AddScoped<IBasketService, BasketService>();
            builder.Services.Configure<RedisSetting>(builder.Configuration.GetSection("RedisSettings"));
            builder.Services.AddSingleton<RedisService>(sp =>
            {
                var redisSetting = sp.GetRequiredService<IOptions<RedisSetting>>().Value;
                var redis = new RedisService(redisSetting.Host, redisSetting.Port);
                redis.Connect();
                return redis;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}