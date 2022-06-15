namespace IMuseum.Persistence.Models;

/// <summary>
/// Status of an loan application
/// </summary>
public enum LoanStatus
{
    /// <summary>
    /// The artwork was requested but not yet approved
    /// </summary>
    OnWait,
    /// <summary>
    /// The loan was approved and the artwork is curently
    /// on loan
    /// </summary>
    OnLoan,
    /// <summary>
    /// The application was approved and the loan time 
    /// finished
    /// </summary>
    Finished,
    /// <summary>
    /// The application was denied.
    /// </summary>
    Denied,
}