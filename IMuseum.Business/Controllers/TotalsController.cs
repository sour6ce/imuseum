using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.LoanApplications;
using IMuseum.Persistence.Repositories.Users;
using IMuseum.Business.Dtos.Totals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IMuseum.Auth.Authorization;

namespace IMuseum.Business.Controllers;

//GET /totals
[Admin]
[ApiController]
[Route("totals")]
public class TotalsController : ControllerBase
{
    private readonly IArtworksRepository artRepository;
    private readonly ILoanApplicationsRepository loanAppRespository;

    public TotalsController(IArtworksRepository artRepository, ILoanApplicationsRepository loanAppRespository)
    {
        this.artRepository = artRepository;
        this.loanAppRespository = loanAppRespository;
    }

    //GET /artists
    [HttpGet]
    public async Task<TotalGetReturnDto> GetTotalsAsync()
    {
        IEnumerable<Artwork> artworks = await artRepository.GetObjectsAsync();
        int countLoanApplications = await loanAppRespository.GetCountAsync();
        int totalArtworks = artRepository.Count;
        int countOnLoan = 0, countInRestoration = 0, countInStorage = 0, countOnDisplay = 0;

        foreach(Artwork art in artworks)
        {
            if(art.CurrentSatus == Artwork.ArtworkStatus.OnLoan)
                countOnLoan++;
            else if(art.CurrentSatus == Artwork.ArtworkStatus.InRestoration)
                countInRestoration++;
            else if(art.CurrentSatus == Artwork.ArtworkStatus.InStorage)
                countInStorage++;
            else if(art.CurrentSatus == Artwork.ArtworkStatus.OnDisplay)
                countOnDisplay++;
        }

        return new TotalGetReturnDto()
        {
            TotalArtworks = totalArtworks,
            CountInRestoration = countInRestoration,
            CountLoanApplications = countLoanApplications,
            CountOnDisplay = countOnDisplay,
            CountOnLoan = countOnLoan,
            CountInStorage = countInStorage
        };
    }
}