using IMuseum.Business.Dtos.Artworks;
using IMuseum.Business.Dtos.LoanApplications;
using IMuseum.Business.Dtos.Restorations;
using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos;

public interface IConvertionService
{
    Artwork ArtworkFromDto(ArtworkPutPostDto dto);
    Task<ArtworkGeneralDto> ArtworkAsDto(Artwork art);


    Task<ArtworkType?> ArtType(int artId);
    Task<bool> IsSculpture(int artId);
    Task<bool> IsPainting(int artId);

    Restoration RestorationFromDto(RestorationReturnDto dto);
    Task<LoanApplicationGeneralDto> LoanAppAsDto(LoanApplication loanApp);

    int? MuseumToId(string name);
    string? MuseumFromId(int id);

    int? RoomToId(string name);
    string? RoomFromId(int id);
}