namespace IMuseum.Business.Dtos.Loans;

public record LoanGetReturnDto
{
    public LoanGeneralDto[] Loans { get; set; }
    public int Count { get; set; }
}