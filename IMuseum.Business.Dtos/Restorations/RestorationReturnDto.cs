using IMuseum.Business.Dtos.Artworks;

namespace IMuseum.Business.Dtos.Restorations;

public record RestorationReturnDto
{
    // artwork: Artwork
    // startDate: Date,
    // dueDate: null
    // restorationStatus: status

    public SimpleIdDto? Artwork { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? DueDate { get; set; } = null;

    public RestorationStatus? RestorationStatus { get; set; }

    public IMuseum.Persistence.Models.Restoration.RestorationType? RestorationType { get; set; }
}