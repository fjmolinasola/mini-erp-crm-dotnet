using Microsoft.EntityFrameworkCore;
using MiniErpCrm.Api.Entities;

namespace MiniErpCrm.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
}
