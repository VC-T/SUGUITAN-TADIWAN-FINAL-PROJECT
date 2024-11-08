using Microsoft.EntityFrameworkCore;
using BookShopAPI.Models;

namespace BookShopAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Bill> Bills { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("UserTbl");
            modelBuilder.Entity<Book>().ToTable("BookTbl");
            modelBuilder.Entity<Bill>().ToTable("BillTbl");
        }
    }
}
