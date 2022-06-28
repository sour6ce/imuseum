using IMuseum.Persistence.Models;

namespace IMuseum.Business.Dtos;

public static class Utils
{
    public static (Dictionary<string, Artworks.ArtworkType>, Dictionary<Artworks.ArtworkType, string>) ArtworkTypeNameMaps()
    {
        var one = new Dictionary<string, Artworks.ArtworkType>
        {
            {"painting", Artworks.ArtworkType.Painting},
            {"sculpture",Artworks.ArtworkType.Sculpture},
            {"other",Artworks.ArtworkType.Other}
        };
        var two = one.Select(x => x.Key).ToDictionary(x => one[x]);

        return (one, two);
    }
    public static (Dictionary<string, Artwork.ArtworkStatus>, Dictionary<Artwork.ArtworkStatus, string>) ArtworkStatusNameMaps()
    {
        var one = new Dictionary<string, Artwork.ArtworkStatus>
        {
            {"on-loan", Artwork.ArtworkStatus.OnLoan},
            {"in-restoration", Artwork.ArtworkStatus.InRestoration},
            {"in-storage", Artwork.ArtworkStatus.InStorage},
            {"on-display", Artwork.ArtworkStatus.OnDisplay},
            {"out", Artwork.ArtworkStatus.Out}
        };
        var two = one.Select(x => x.Key).ToDictionary(x => one[x]);

        return (one, two);
    }
    public static (Dictionary<string, LoanApplication.LoanApplicationStatus>, Dictionary<LoanApplication.LoanApplicationStatus, string>) LoanAppStatusNameMap()
    {
        var one = new Dictionary<string, LoanApplication.LoanApplicationStatus>
        {
            {"on-wait", LoanApplication.LoanApplicationStatus.OnWait},
            {"on-loan", LoanApplication.LoanApplicationStatus.OnLoan},
            {"finished", LoanApplication.LoanApplicationStatus.Finished},
            {"denied", LoanApplication.LoanApplicationStatus.Denied}
        };
        var two = one.Select(x => x.Key).ToDictionary(x => one[x]);

        return (one, two);
    }
    public static (Dictionary<string, Restoration.RestorationType>, Dictionary<Restoration.RestorationType, string>) RestorationTypeNameMap()
    {
        var one = new Dictionary<string, Restoration.RestorationType>
        {
            {"scientific", Restoration.RestorationType.Scientific},
            {"aesthetic-functional", Restoration.RestorationType.AestheticFunctional},
            {"commercial", Restoration.RestorationType.Commercial},
            {"other", Restoration.RestorationType.Other}
        };
        var two = one.Select(x => x.Key).ToDictionary(x => one[x]);

        return (one, two);
    }
}