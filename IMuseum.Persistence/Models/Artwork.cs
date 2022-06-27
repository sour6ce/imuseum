using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IMuseum.Persistence.Models;

[Table("Artworks")]
public record Artwork : DatabaseModel
{
    /// <summary>
    /// Status of an stored artwork.
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Internal artwork that was loaned to another
        /// museum
        /// </summary>
        OnLoan,
        /// <summary>
        /// Internal artwork that was send to restoration.
        /// </summary>
        InRestoration,
        /// <summary>
        /// Internal artwork that is in storage and not
        /// at exhibition in any room.
        /// </summary>
        InStorage,
        /// <summary>
        /// Internal or external artwork that is at 
        /// exhibition.
        /// </summary>
        OnDisplay,
        /// <summary>
        /// External artwork registered and meant to be in the
        /// museum but has not arrived yet.
        /// </summary>
        OnWaitArrival,
        /// <summary>
        /// Internal or external artwork that is out of the 
        /// museum and has no intentions to return to currently.
        /// </summary>
        Out
    }
    public string Title { get; set; }
    public string Author { get; set; }
    /// <summary>
    /// Date the artwork was created by the artist.
    /// </summary>
    public DateTime CreationDate { get; set; }
    /// <summary>
    /// Date and Time the artwork was incorporated to the Museum. In case
    /// of an external artwork this can be the time it arrives to the museum.
    /// Has <c>null</c> if the artwork is registered but not yet in the museum.
    /// </summary>
    public DateTime? IncorporatedDate { get; set; } = DateTime.Now;
    /// <summary>
    /// Artistic period of the artwork.
    /// </summary>
    public string Period { get; set; }
    public decimal Assessment { get; set; }
    /// <summary>
    /// Status of the artwork inside or outside the museum.
    /// </summary>
    public Status CurrentSatus { get; set; } = Status.InStorage;
    /// <summary>
    /// The museum owner of the artwork, in case of an internal artwork this
    /// field must be <c>null</c>.
    /// </summary>
    [ForeignKey("Museum")]
    public int MuseumId{get; set;}
    public Museum? Museum { get; set; } = null;

    public string Images { get; set; }
    public string Description { get; set; }

    /// <summary>
    /// Room in wich the Artwork is displayed. (Related from relation to Rooms)
    /// </summary>
    [ForeignKey("Room")]
    public int RoomId{get;set;}
    public Room? Room { get; set; }
    /// <summary>
    /// All the restorations of the Artwork. (Related from relation to Restorations)
    /// </summary>
    public ICollection<Restoration> Restorations { get; set; }
    /// <summary>
    /// All loan applications of this Artwork. (Related from relation to Loan Applications)
    /// </summary>
    public ICollection<LoanApplication> LoanApplications { get; set; }
}