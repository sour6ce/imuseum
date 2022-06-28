using IMuseum.Business.Dtos.Artworks;
using IMuseum.Persistence.Models;

public interface IConvertionService
{
    Artwork ArtworkFromDto(ArtworkPutPostDto dto);
    Task<ArtworkGeneralDto> ArtworkAsDto(Artwork art);


    Task<ArtworkType?> ArtType(int artId);
    Task<bool> IsSculpture(int artId);
    Task<bool> IsPainting(int artId);

}