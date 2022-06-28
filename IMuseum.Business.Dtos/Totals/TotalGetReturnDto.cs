namespace IMuseum.Business.Dtos.Totals;

public record TotalGetReturnDto
{
    public int TotalArtworks { get; set; }
    public int CountOnLoan { get; set; }
    public int CountInRestoration { get; set; }
    public int CountInStorage { get; set; }
    public int CountOnDisplay { get; set; }
    public int CountLoanApplications { get; set; }
}