using Microsoft.EntityFrameworkCore;

namespace Banking_API.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
       
        public DbSet<UserLoginDetail> UserLogIn { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<UserAccountDetail> UserAccountDetails { get; set; }
        public DbSet<TransactionDetail> Transactions { get; set; }
        public DbSet<PayeeDetail> PayeeDetails { get; set; }
    }
}
