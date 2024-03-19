using Microsoft.EntityFrameworkCore;
using VismaTask.Models;

namespace EFCoreInMemoryDbDemo;

public class ApiContext : DbContext
{
    public DbSet<Employee> Employees { get; set; }

    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
    }

    /*protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(new Employee(1, "Url1"));
        modelBuilder.Entity<Employee>().HasData(new Employee(2, "Url2"));
        modelBuilder.Entity<Employee>().HasData(new Employee(3, "Url3"));
        modelBuilder.Entity<Employee>().HasData(new Employee(4, "Url4"));
    }*/
}