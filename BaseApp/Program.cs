using BaseApp.DataAccess;
using BaseApp.Models;

namespace BaseApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddMediatR(config => config.RegisterServicesFromAssemblies(typeof(Program).Assembly));
            builder.Services.AddTransient<IRepository<WeatherForecast>, WeatherForecastRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}