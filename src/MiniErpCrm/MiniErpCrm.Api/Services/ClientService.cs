using MiniErpCrm.Api.Dtos;

namespace MiniErpCrm.Api.Services;

public class ClientService
{
    public IReadOnlyList<ClientDto> GetAll()
    {
        return new List<ClientDto>
        {
            new() { Id = 1, Name = "Cliente Demo 1" },
            new() { Id = 2, Name = "Cliente Demo 2" }
        };
    }
}
