using System.ComponentModel.DataAnnotations;

namespace MiniErpCrm.Api.Dtos;

public sealed class CreateClientRequest
{
    [Required]
    [MinLength(2)]
    [MaxLength(200)]
    public string Name { get; init; } = "";
}
