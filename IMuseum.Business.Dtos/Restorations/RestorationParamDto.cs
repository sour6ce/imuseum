namespace IMuseum.Business.Dtos.Restorations;

public record RestorationParamDto
{
    public IMuseum.Persistence.Models.Restoration.RestorationType RestorationType { get; set; }
}