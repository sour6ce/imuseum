using IMuseum.Business.Dtos.Artworks;
using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Artworks;

namespace IMuseum.Business.Dtos;

public class ConvertionService : IConvertionService
{
    public Task<ArtworkType?> ArtType(int artId)
    {
        throw new NotImplementedException();
    }

    public Task<ArtworkGeneralDto> ArtworkAsDto(Artwork art)
    {
        throw new NotImplementedException();
    }

    public Artwork ArtworkFromDto(ArtworkPutPostDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsPainting(int artId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsSculpture(int artId)
    {
        throw new NotImplementedException();
    }
}