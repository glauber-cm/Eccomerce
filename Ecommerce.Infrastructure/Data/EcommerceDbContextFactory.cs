using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.Infrastructure.Data
{
    public class EcommerceDbContextFactory : IDesignTimeDbContextFactory<EcommerceDbContext>
    {
        public EcommerceDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EcommerceDbContext>();

            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EcommerceDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new EcommerceDbContext(optionsBuilder.Options);

        }
    }
}
