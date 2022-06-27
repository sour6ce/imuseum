using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.LoanApplications;
using IMuseum.Persistence.Repositories.Loans;
using IMuseum.Business.Dtos.LoanApplications;
using IMuseum.Business.Dtos.Loans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;
namespace IMuseum.Business.Controllers;

//GET /loan-apps
[Director]
[ApiController]
[Route("loan-apps")]
public class LoanApplicationsController : ControllerBase
{
    private readonly ILoanApplicationsRepository loanAppsRepository;
    private readonly ILoansRepository loansRepository;

    public LoanApplicationsController(ILoanApplicationsRepository loanAppsRepository)
    {
        this.loanAppsRepository = loanAppsRepository;
    }

    internal async Task<LoanApplicationGeneralDto> LoanAppAsDto(LoanApplication loanApp)
    {
        return new LoanApplicationGeneralDto(){
            Id = loanApp.Id,
            ApplicationDate = loanApp.ApplicationDate,
            Duration = loanApp.Duration,
            LoanApplicationStatus = loanApp.CurrentStatus,
            ArtworkId = loanApp.ArtworkId,
            MuseumId = loanApp.MuseumId
        };
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
    public async Task<ActionResult<LoanApplicationGeneralDto>> CreateLoanAsync(int id,[FromQuery] decimal payment)
    {
        // TODO: Add update redirecting in case it exist
        var loanApp = await loanAppsRepository.GetObjectAsync(id);
        if(loanApp is null)
            return NoContent();

        var result=loanAppsRepository.ExecuteOnDbAsync(async (set,context)=>{
            var result=await set.FirstOrDefaultAsync((x)=>x.Id==id);
            if (result==null)
                return false;
            else {
                result.CurrentStatus = LoanApplication.LoanApplicationStatus.OnLoan;
                await context.SaveChangesAsync();
                return true;
            }
        });

        var loan = new Loan(){
            StartDate = DateTime.UtcNow,
            PaymentAmount = payment,
            LoanApplicationId = id,
            Application = await loanAppsRepository.GetObjectAsync(id)
        };
        await loansRepository.AddAsync(loan);
        return CreatedAtAction(nameof(CreateLoanAsync), new Uri($"{Request.Path}/{loan.Id}"), (new LoanGeneralDto(){PaymentAmount = loan.PaymentAmount, LoanApplicationId = loan.LoanApplicationId} ));
    }

     //POST /loan-apps/{id}/reject
    [HttpPost]
    [Route("{id}/reject")]
    public async Task<ActionResult<LoanApplicationGeneralDto>> RejectLoanAsync(int id)
    {
        // TODO: Add update redirecting in case it exist
        var loanApp = await loanAppsRepository.GetObjectAsync(id);
        if(loanApp is null)
            return NoContent();

        var result=loanAppsRepository.ExecuteOnDbAsync(async (set,context)=>{
            var result=await set.FirstOrDefaultAsync((x)=>x.Id==id);
            if (result==null)
                return false;
            else {
                result.CurrentStatus = LoanApplication.LoanApplicationStatus.Denied;
                await context.SaveChangesAsync();
                return true;
            }
        });

        return CreatedAtAction(nameof(RejectLoanAsync), new Uri($"{Request.Path}/{loanApp.Id}"), loanApp);
    }
}