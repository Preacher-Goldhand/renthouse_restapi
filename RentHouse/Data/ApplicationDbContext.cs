using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RentHouse.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) {}
        
        public DbSet<MachineModel> Machines { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<UserModel> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configure your database provider here (e.g., UseSqlite, UseSqlServer).
            // optionsBuilder.UseSqlite("DataSource=app.sqlite;Cache=Shared");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<OrderModel>()
                .HasOne(u => u.User)
                .WithMany(o => o.Orders)
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder
                .Entity<OrderModel>()
                .HasOne(m => m.Machine)
                .WithMany()
                .HasForeignKey("MachineId")
                .OnDelete(DeleteBehavior.NoAction);
    
        }
    }
    

}
