using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.LoanApplications;
using IMuseum.Persistence.Repositories.Loans;
using IMuseum.Business.Dtos.LoanApplications;
using IMuseum.Business.Dtos.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;
using IMuseum.Business.Dtos;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Sculptures;
using IMuseum.Persistence.Repositories.Paintings;

namespace IMuseum.Business.Controllers;

//GET /loan-apps
[Director]
[ApiController]
[Route("loan-apps")]
public class LoanApplicationsController : ControllerBase
{
    private readonly ILoanApplicationsRepository loanAppsRepository;
    private readonly ILoansRepository loansRepository;
    private readonly IConvertionService convertionService;

    public LoanApplicationsController(IArtworksRepository artworks, ISculpturesRepository sculptures,
     IPaintingsRepository paints,ILoanApplicationsRepository loanAppsRepository, ILoansRepository loansRepository)
    {
        this.convertionService = new ConvertionService(artworks, sculptures, paints);
        this.loansRepository = loansRepository;
        this.loanAppsRepository = loanAppsRepository;
    }

    internal async Task<LoanApplicationGeneralDto> LoanAppAsDto(LoanApplication loanApp)
    {
        return new LoanApplicationGeneralDto()
        {
            Id = loanApp.Id,
            ApplicationDate = loanApp.ApplicationDate,
            Duration = loanApp.Duration,
            LoanApplicationStatus = loanApp.CurrentStatus,
            Artwork = await convertionService.ArtworkAsDto(loanApp.Artwork),
            ArtworkId = loanApp.ArtworkId,
            MuseumId = loanApp.MuseumId
        };
    }

    internal LoanApplication LoanAppFromDto(LoanApplicationPutPostDto dto)
    {
        LoanApplication loanApp = new LoanApplication()
        {
            ArtworkId = dto.ArtworkId,
            Duration = dto.Duration,
            ApplicationDate = dto.ApplicationDate,
            MuseumId = dto.MuseumId
        };

        return loanApp;
    }

    //POST /loan-apps
    [HttpPost]
    public async Task<ActionResult<LoanApplicationGeneralDto>> CreateLoanAppAsync(LoanApplicationPutPostDto dto)
    {
        var loanApp = new LoanApplication()
        {
            ApplicationDate = dto.ApplicationDate,
            Duration = dto.Duration,
            CurrentStatus = LoanApplication.LoanApplicationStatus.OnWait,
            ArtworkId = dto.ArtworkId,
            MuseumId = dto.MuseumId
        };
        await loanAppsRepository.AddAsync(loanApp);
        return CreatedAtAction(nameof(CreateLoanAsync), null, dto);
    }

    //PUT /loan-apps/{id}
    [HttpPut]
    [Route("{id}")]
    public async Task<ActionResult> UpdateArtworkAsync(int id, LoanApplicationPutPostDto dto)
    {
        var loanApp = LoanAppFromDto(dto);
        var found = loanAppsRepository.GetObjectAsync(id);
        if (await found == null)
            return NotFound();


        await loanAppsRepository.UpdateObjectAsync(loanApp);
        return AcceptedAtAction(nameof(UpdateArtworkAsync), dto);
    }

    //GET /loan-apps
    [HttpGet]
    public async Task<LoanApplicationGetReturnDto> GetLoanAppsAsync([FromQuery] LoanApplicationGetParamDto args)
    {
        var filtered = (DbSet<LoanApplication> all) =>
        {
            return
            all.Where((x) => x.ArtworkId == args.ArtworkId)
            .Where((x) => args.MuseumId == x.MuseumId)
            .Where((x) => args.Status == x.CurrentStatus);
        };
        var count = (loanAppsRepository.ExecuteOnDbAsync(async (all) =>
        {
            return
            await filtered(all).CountAsync();
        }));
        var loanApps = (loanAppsRepository.ExecuteOnDb((all) =>
        {
            return
            filtered(all).Skip(args.PageSize * (args.Page - 1))
            .Take(args.PageSize).ToArray();
        }));
        return new LoanApplicationGetReturnDto()
        {
            LoanApps = (loanApps).Select((x) => this.LoanAppAsDto(x)).ToArray().Select((x) => x.Result).ToArray(),
            Count = (await count)
        };
    }

    //POST /loan-apps/{id}/accept
    [HttpPost]
    [Route("{id}/accept")]
    public async Task<ActionResult<LoanApplicationGeneralDto>> CreateLoanAsync(int id, [FromQuery] decimal payment)
    {
        var loanApp = await loanAppsRepository.GetObjectAsync(id);
        if (loanApp is null)
            return NotFound();

        var result = loanAppsRepository.ExecuteOnDbAsync(async (set, context) =>
        {
            var result = await set.FirstOrDefaultAsync((x) => x.Id == id);
            if (result == null)
                return false;
            else
            {
                result.CurrentStatus = LoanApplication.LoanApplicationStatus.OnLoan;
                await context.SaveChangesAsync();
                return true;
            }
        });

        var loan = new Loan()
        {
            StartDate = DateTime.UtcNow,
            PaymentAmount = payment,
            LoanApplicationId = id,
        };
        await loansRepository.AddAsync(loan);
        return CreatedAtAction(nameof(CreateLoanAsync), null,
         (new LoanGeneralDto()
         {
             PaymentAmount = loan.PaymentAmount,
             LoanApplicationId = loan.LoanApplicationId,
             StartDate = loan.StartDate
         }));
    }

    //POST /loan-apps/{id}/reject
    [HttpPost]
    [Route("{id}/reject")]
    public async Task<ActionResult<LoanApplicationGeneralDto>> RejectLoanAsync(int id)
    {
        var loanApp = await loanAppsRepository.GetObjectAsync(id);
        if (loanApp is null)
            return NotFound();

        var result = loanAppsRepository.ExecuteOnDbAsync(async (set, context) =>
        {
            var result = await set.FirstOrDefaultAsync((x) => x.Id == id);
            if (result == null)
                return false;
            else
            {
                result.CurrentStatus = LoanApplication.LoanApplicationStatus.Denied;
                await context.SaveChangesAsync();
                return true;
            }
        });

        return new OkResult();
    }
}