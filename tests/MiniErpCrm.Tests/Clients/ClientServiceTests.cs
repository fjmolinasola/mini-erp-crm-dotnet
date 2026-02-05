using Microsoft.EntityFrameworkCore;
using MiniErpCrm.Api.Data;
using MiniErpCrm.Api.Dtos;
using MiniErpCrm.Api.Services;
using Xunit;

namespace MiniErpCrm.Tests.Clients;

public class ClientServiceTests
{
    private static AppDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_CreatesClient_AndReturnsDto()
    {
        // Arrange
        using var db = CreateInMemoryDb();
        var service = new ClientService(db);

        var request = new CreateClientRequest { Name = "Cliente Test" };

        // Act
        var created = await service.CreateAsync(request);

        // Assert
        Assert.True(created.Id > 0);
        Assert.Equal("Cliente Test", created.Name);

        var inDb = await db.Clients.FirstOrDefaultAsync(c => c.Id == created.Id);
        Assert.NotNull(inDb);
        Assert.Equal("Cliente Test", inDb!.Name);
    }

    [Fact]
    public async Task UpdateAsync_WhenClientExists_UpdatesAndReturnsDto()
    {
        // Arrange
        using var db = CreateInMemoryDb();

        // Seed
        db.Clients.Add(new MiniErpCrm.Api.Entities.Client { Name = "Antes" });
        await db.SaveChangesAsync();
        var existing = await db.Clients.FirstAsync();

        var service = new ClientService(db);
        var request = new UpdateClientRequest { Name = "Después" };

        // Act
        var updated = await service.UpdateAsync(existing.Id, request);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(existing.Id, updated!.Id);
        Assert.Equal("Después", updated.Name);

        var inDb = await db.Clients.FindAsync(existing.Id);
        Assert.NotNull(inDb);
        Assert.Equal("Después", inDb!.Name);
    }

    [Fact]
    public async Task DeleteAsync_WhenClientExists_DeletesAndReturnsTrue()
    {
        // Arrange
        using var db = CreateInMemoryDb();

        // Seed
        db.Clients.Add(new MiniErpCrm.Api.Entities.Client { Name = "Para borrar" });
        await db.SaveChangesAsync();
        var existing = await db.Clients.FirstAsync();

        var service = new ClientService(db);

        // Act
        var deleted = await service.DeleteAsync(existing.Id);

        // Assert
        Assert.True(deleted);
        var inDb = await db.Clients.FindAsync(existing.Id);
        Assert.Null(inDb);
    }

}
