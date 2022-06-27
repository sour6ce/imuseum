namespace IMuseum.Business.Dtos.LoanApplications;

public record LoanApplicationGetReturnDto
{
    public LoanApplicationGeneralDto[] LoanApps { get; set; }
    public int Count { get; set; }
}