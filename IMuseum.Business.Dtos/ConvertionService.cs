using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Business.Dtos.Artworks;
using IMuseum.Persistence.Models;
using IMuseum.Persistence.Repositories.Paintings;
using IMuseum.Persistence.Repositories.Sculptures;

namespace IMuseum.Business.Dtos;

public class ConvertionService : IConvertionService
{
    private readonly IArtworksRepository artRepository;
    private readonly ISculpturesRepository sculpturesRepository;
    private readonly IPaintingsRepository paintsRepository;
    public ConvertionService(IArtworksRepository artworks, ISculpturesRepository sculptures, IPaintingsRepository paints)
    {
        this.artRepository = artworks;
        this.sculpturesRepository = sculptures;
        this.paintsRepository = paints;
    }

    public async Task<ArtworkGeneralDto> ArtworkAsDto(Artwork art)
    {
        var sc = sculpturesRepository.GetObjectAsync(art.Id);
        var pnt = paintsRepository.GetObjectAsync(art.Id);

        var dto = new ArtworkGeneralDto()
        {
            Id = art.Id.GetHashCode(),
            Title = art.Title,
            Description = art.Description,
            Author = art.Author,
            CreationDate = art.CreationDate,
            IncorporatedDate = art.IncorporatedDate,
            Period = art.Period,
            Assessment = art.Assessment,
            Status = art.CurrentSatus,
            Type = ArtType(art.Id).Result.Value
        };

        switch (dto.Type)
        {
            case ArtworkType.Sculpture:
                var tempsc = await sc;
                dto.Style = tempsc?.Style;
                dto.Material = tempsc?.Material;
                break;
            case ArtworkType.Painting:
                var temppnt = await pnt;
                dto.Style = temppnt?.Style;
                dto.Media = temppnt?.Media;
                break;
            default:
                break;
        }
        return dto;
    }

    public Artwork ArtworkFromDto(ArtworkPutPostDto dto)
    {

        switch (dto.Type)
        {
            case ArtworkType.Sculpture:
                var sc = new Sculpture()
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Description = dto.Description,
                    CreationDate = dto.CreationDate,
                    IncorporatedDate = dto.IncorporatedDate,
                    Period = dto.Period,
                    Assessment = dto.Assessment,
                    Style = dto.Style,
                    Image = dto.Image,
                    Material = dto.Material
                };
                return sc;
            case ArtworkType.Painting:
                var pnt = new Painting()
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Description = dto.Description,
                    CreationDate = dto.CreationDate,
                    IncorporatedDate = dto.IncorporatedDate,
                    Period = dto.Period,
                    Assessment = dto.Assessment,
                    Style = dto.Style,
                    Image = dto.Image,
                    Media = dto.Media
                };
                return pnt;
            default:
                var art = new Artwork()
                {
                    Title = dto.Title,
                    Author = dto.Author,
                    Description = dto.Description,
                    CreationDate = dto.CreationDate,
                    IncorporatedDate = dto.IncorporatedDate,
                    Period = dto.Period,
                    Image = dto.Image,
                    Assessment = dto.Assessment
                };
                return art;
        }
    }

    public async Task<bool> IsPainting(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return false;
        return await this.paintsRepository.ContainsAsync(artId);
    }

    public async Task<bool> IsSculpture(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return false;
        return await this.sculpturesRepository.ContainsAsync(artId);
    }

    public async Task<ArtworkType?> ArtType(int artId)
    {
        bool isArt = await this.artRepository.ContainsAsync(artId);
        if (!isArt)
            return null; //Isn't the Id of an artwork
        // NOTE: Here goes analysis to get a string that identifies the type of the artwork
        return (await IsSculpture(artId)) ? ArtworkType.Sculpture :
                (await IsPainting(artId)) ? ArtworkType.Painting :
                ArtworkType.Other;
    }
}