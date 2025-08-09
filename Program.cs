
using ManagementApi.Data;
using Microsoft.EntityFrameworkCore;

namespace ManagementApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseInMemoryDatabase("db");
        });


        builder.Services.AddControllers();
        builder.Services.AddOpenApi();


        builder.Services.AddCors(options =>
        {
            options.AddPolicy("cors", policy =>
            {
                policy.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin();

            });
        });

        var app = builder.Build();


        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            if (!context.Positions.Any())
            {
                context.Positions.AddRange(DataSeed.SeedPositions());
                context.SaveChanges();
            }

            if (!context.Employees.Any())
            {
                context.Employees.AddRange(DataSeed.SeedEmployees());
                context.SaveChanges();
            }

            if (!context.OrganizationUnits.Any())
            {
                context.OrganizationUnits.AddRange(DataSeed.SeedOrganizationUnits());
                context.SaveChanges();
            }
        }


        app.UseCors("cors");
        app.MapOpenApi();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.Run();
    }
}
