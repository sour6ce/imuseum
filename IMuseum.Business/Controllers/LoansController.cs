using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Loans;
using IMuseum.Business.Dtos.LoanApplications;
using IMuseum.Business.Dtos.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;

namespace IMuseum.Business.Controllers;

//GET /loans
[Director]
[ApiController]
[Route("loans")]
public class LoanController : ControllerBase
{
    private readonly ILoansRepository loansRepository;

    public LoanController(ILoansRepository loansRepository)
    {
        this.loansRepository = loansRepository;
    }

    internal LoanGeneralDto LoanAsDto(Loan loan)
    {
        return new LoanGeneralDto()
        {
            PaymentAmount = loan.PaymentAmount,
            LoanApplicationId = loan.LoanApplicationId,
            StartDate = loan.StartDate
        };
    }

    //GET /loans
    [HttpGet]
    public async Task<LoanGetReturnDto> GetLoanAppsAsync([FromQuery] LoanGetParamDto args)
    {
        var filtered = (DbSet<Loan> all) =>
        {
            return
            all.Where((x) => x.Application.ArtworkId == args.ArtworkId)
            .Where((x) => args.MuseumId == x.Application.MuseumId)
            .Where((x) => args.IncomeMin <= x.PaymentAmount && args.IncomeMax >= x.PaymentAmount);
        };
        var count = (loansRepository.ExecuteOnDbAsync(async (all) =>
        {
            return
            await filtered(all).CountAsync();
        }));
        var loans = (loansRepository.ExecuteOnDb((all) =>
        {
            return
            filtered(all).Skip(args.PageSize * (args.Page - 1))
            .Take(args.PageSize).ToArray();
        }));
        return new LoanGetReturnDto()
        {
            Loans = (loans).Select((x) => this.LoanAsDto(x)).ToArray(),
            Count = (await count)
        };
    }
}