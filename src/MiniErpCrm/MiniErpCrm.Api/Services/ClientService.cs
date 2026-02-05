using Microsoft.EntityFrameworkCore;
using MiniErpCrm.Api.Data;
using MiniErpCrm.Api.Dtos;

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
}