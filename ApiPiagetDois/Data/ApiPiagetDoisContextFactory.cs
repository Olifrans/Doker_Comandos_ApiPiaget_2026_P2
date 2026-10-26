using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ApiPiagetDois.Data
{
    public class ApiPiagetDoisContextFactory : IDesignTimeDbContextFactory<ApiPiagetDoisContext>
    {
        public ApiPiagetDoisContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApiPiagetDoisContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ApiPiagetDoisContext"));

            return new ApiPiagetDoisContext(optionsBuilder.Options);
        }
    }
}