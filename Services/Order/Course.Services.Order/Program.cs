
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using Course.SharedLibrary.Services.Abstract;
using Course.SharedLibrary.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Features.Orders.Commands;
using Order.Application.Features.Orders.Queries;
using Order.Infrastructure;

namespace Course.Services.Order
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddHttpContextAccessor();
            // Add services to the container.
            var requiredAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");
            builder.Services.AddControllers(opt=>opt.Filters.Add(new AuthorizeFilter(requiredAuthorizePolicy)));
            builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = builder.Configuration["IdentityServerUrl"];
                opt.Audience = "resource_order";
                opt.RequireHttpsMetadata = false;
            });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblyContaining(typeof(GetOrdersByUserIdQuery)));
            builder.Services.AddInfrastructureServices(builder.Configuration);
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