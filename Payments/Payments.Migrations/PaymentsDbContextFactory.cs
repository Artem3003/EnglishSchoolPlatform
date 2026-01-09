using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Payments.Domain.Data;

namespace Payments.Migrations;

public class PaymentsDbContextFactory : IDesignTimeDbContextFactory<PaymentsDbContext>
{
    public PaymentsDbContext CreateDbContext(string[] args)
    {
        var configuration = new Microsoft.Extensions.Configuration.ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<PaymentsDbContext>();
        var connectionString = configuration.GetConnectionString("PaymentsConnection");

        optionsBuilder.UseSqlServer(connectionString,
            b => b.MigrationsAssembly("Payments.Migrations"));

        return new PaymentsDbContext(optionsBuilder.Options);
    }
}
