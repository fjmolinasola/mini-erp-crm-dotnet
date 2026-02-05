using MiniErpCrm.Api.Entities;

namespace MiniErpCrm.Api.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext db)
    {
        if (db.Clients.Any()) return;

        db.Clients.AddRange(
            new Client { Name = "Cliente BD 1" },
            new Client { Name = "Cliente BD 2" }
        );

        db.SaveChanges();
    }
}