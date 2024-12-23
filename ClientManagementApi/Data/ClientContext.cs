using ClientManagementApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientManagementApi.Data
{
    public class ClientContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Founder> Founders { get; set; }

        public ClientContext(DbContextOptions<ClientContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Founders)
                .WithOne(f => f.Client)
                .HasForeignKey(f => f.ClientId);

        }
    }

}
