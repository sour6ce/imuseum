using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories.LoanApplications;

public interface ILoanApplicationsRepository : IRepository<LoanApplication>
{
    Task UpdateLoanApplicationAsync(LoanApplication item);
}