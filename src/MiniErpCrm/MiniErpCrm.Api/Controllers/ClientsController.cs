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
    
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateClientRequest request)
    {
        var updated = await _clientService.UpdateAsync(id, request);

        if (updated is null)
            return NotFound();

        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _clientService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

}