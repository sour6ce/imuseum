using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos.Styles;

public record StyleGetReturnDto
{
    public string[] Styles { get; set; }
    public int Count { get; set; }
}