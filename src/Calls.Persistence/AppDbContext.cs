using Calls.Domain.Entities;
using Calls.Persistence.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Calls.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Call> Calls { get; set; }
    public DbSet<PhoneNumber> PhoneNumbers { get; set; }
    
    public AppDbContext() => Database.EnsureCreated();
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=calls.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new ContactConfiguration());
        modelBuilder.ApplyConfiguration(new CallConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneNumberConfiguration());
    }
}