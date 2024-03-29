using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Users;

public record UserGetReturnDto
{
    public UserPostAndPutDto[] Users { get; set; }
    public int Count { get; set; }
}