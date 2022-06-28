using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Users;

public record UserPostAndPutDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public int RoleId { get; set; }
    public string Role { get; set; }
}