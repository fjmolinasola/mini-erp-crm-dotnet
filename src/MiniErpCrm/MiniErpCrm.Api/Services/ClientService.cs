using Microsoft.EntityFrameworkCore;
using MiniErpCrm.Api.Data;
using MiniErpCrm.Api.Dtos;
using MiniErpCrm.Api.Entities;

namespace MiniErpCrm.Api.Services;

public class ClientService
{
    private readonly AppDbContext _db;
    public ClientService(AppDbContext db)
    {
        _db = db;
    }
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        return await _db.Clients
            .AsNoTracking()
            .Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }
    public async Task<ClientDto> CreateAsync(CreateClientRequest request)
    {
        var client = new Client
        {
            Name = request.Name
        };

        _db.Clients.Add(client);
        await _db.SaveChangesAsync();

        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name
        };
    }
}