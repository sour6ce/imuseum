namespace IMuseum.Business.Dtos.Users;

public record UserGetParamDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
}