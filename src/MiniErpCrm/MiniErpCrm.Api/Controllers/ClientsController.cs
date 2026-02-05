using Microsoft.AspNetCore.Mvc;
using MiniErpCrm.Api.Dtos;
using MiniErpCrm.Api.Services;

namespace MiniErpCrm.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ClientsController : ControllerBase
{
    private readonly ClientService _clientService;

    public ClientsController(ClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        var clients = await _clientService.GetAllAsync();
        return Ok(clients);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create([FromBody] CreateClientRequest request)
    {
        var created = await _clientService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetAll),
            new { id = created.Id },
            created
        );
    }
}