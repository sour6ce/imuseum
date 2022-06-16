using IMuseum.Persistence.Models;

namespace IMuseum.Persistence.Repositories.Loans;

public interface ILoansRepository : IRepository<Loan>
{
    Task UpdateLoanAsync(Loan item);
}