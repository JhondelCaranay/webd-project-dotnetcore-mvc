using DotnetMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetMvc.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Item> Items { get; set; }
    public DbSet<Category> Category { get; set; }
}