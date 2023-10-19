using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace Course.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json");
            builder.Services.AddOcelot();
            builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
            {
                options.Audience = "resource_gateway";
                options.Authority = builder.Configuration["IdentityServerUrl"];
                options.RequireHttpsMetadata = false;
            });
            var app = builder.Build();
            Console.WriteLine(builder.Environment.EnvironmentName.ToLower());
            app.UseAuthentication();
            app.UseOcelot().Wait();
            app.Run();
        }
    }
}