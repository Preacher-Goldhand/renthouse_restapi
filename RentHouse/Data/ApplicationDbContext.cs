using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RentHouse.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}
        public DbSet<MachineModel> Machines {get; set;}
        public DbSet<OrderModel> Orders {get; set;}
        public DbSet<UserModel> Users {get; set;}
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlite("DataSource=app.sqlite;Cache=Shared");
        } 
}
