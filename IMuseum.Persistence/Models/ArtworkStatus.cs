namespace IMuseum.Persistence.Models;

/// <summary>
/// Status of an stored artwork.
/// </summary>
public enum ArtworkStatus
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