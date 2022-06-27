using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Users;

public record UserPostAndPutDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public int RoleId { get; set; }
}