using CarRentalCompany.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentalCompany.Data
{
    public class RentCompanyDbContext : DbContext
    {
        public RentCompanyDbContext(DbContextOptions<RentCompanyDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Rent> Rents { get; set; }
    }
}
