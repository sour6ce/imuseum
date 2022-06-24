using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Users;

public record UserPostDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public Role? Role { get; set; }

    // TODO: [Agregar el campo Scope que no se que significa]
}