using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Loans;
using IMuseum.Business.Dtos.LoanApplications;
using IMuseum.Business.Dtos.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;
using IMuseum.Business.Dtos;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;

namespace IMuseum.Business.Controllers;

//GET /loans
[Director]
[ApiController]
[Route("loans")]
public class LoanController : ControllerBase
{
    private readonly IConvertionService convertionService;
    private readonly ILoansRepository loansRepository;

    public LoanController(IArtworksRepository artworks, ISculpturesRepository sculptures,
    IConvertionService convServ,
    IPaintingsRepository paints, ILoansRepository loansRepository)
    {
        this.convertionService = convServ;
        this.loansRepository = loansRepository;
    }

    internal async Task<LoanGeneralDto> LoanAsDto(Loan loan)
    {
        return new LoanGeneralDto()
        {
            PaymentAmount = loan.PaymentAmount,
            LoanApplicationId = loan.LoanApplicationId,
            LoanApplication = await convertionService.LoanAppAsDto(loan.Application),
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
            all.Where((x) => args.ArtworkId == null || x.Application.ArtworkId == args.ArtworkId)
            .Where((x) => args.ArtworkId == null || args.MuseumId == x.Application.MuseumId)
            .Where((x) =>
                (args.IncomeMin == null && args.IncomeMax == null) ||
                (args.IncomeMin != null && args.IncomeMin <= x.PaymentAmount && args.IncomeMax == null) ||
                (args.IncomeMax != null && args.IncomeMin == null && args.IncomeMax >= x.PaymentAmount) ||
                (args.IncomeMin <= x.PaymentAmount && args.IncomeMax >= x.PaymentAmount)
            );
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
            Loans = (loans).Select((x) => this.LoanAsDto(x)).ToArray().Select((x) => x.Result).ToArray(),
            Count = (await count)
        };
    }
}