using Microsoft.EntityFrameworkCore;

namespace bank_bill.Models
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options){}
        public DbSet<Client> Clients { get; set; }

    }
}