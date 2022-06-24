namespace IMuseum.Business.Dtos.Restorations;

public record RestorationGetReturnDto
{
    public RestorationReturnDto[] Restorations { get; set; }
    public int Count { get; set; }
}