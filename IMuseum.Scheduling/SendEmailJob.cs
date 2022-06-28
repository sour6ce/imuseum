using Quartz;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using IMuseum.Persistence.Repositories.Users;
using IMuseum.Persistence.Repositories.Artworks;
using IMuseum.Persistence.Repositories.Loans;
using IMuseum.Persistence.Repositories.Restorations;
using IMuseum.Persistence.Repositories;
using IMuseum.Persistence.Models;
using Microsoft.Extensions.Configuration;

namespace IMuseum.Scheduling;

public class SendMailJob : IJob
{
    private readonly ILogger logger;
    private readonly IConfiguration configuration;
    private readonly IServiceProvider serviceProvider;

    public SendMailJob(IConfiguration configuration, ILogger<SendMailJob> logger, IServiceProvider serviceProvider)
    {
        this.logger = logger;
        this.configuration = configuration;
        this.serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        //Checking artworks on stock that need restoration
        ICollection<Artwork> artworksNeedRestoration = await GetRestorationNeededArtworks();

        //Checking expired loans for artworks from the museum
        ICollection<Artwork> artworksMuseumLoanExpired = await GetMuseumLoanExpiredArtworks();

        //Checking expired loans for arworks from a friend museum
        ICollection<Artwork> artworksFriendMuseumLoanExpired = await GetFriendMuseumsLoanExpiredArtworks();

        //If the is info related to restoration that is needed to be informed, then send the corresponding email to restorations chief
        string messageRestorationsChief = await GetMessageForRestorationsChief(artworksNeedRestoration);
        if (messageRestorationsChief != "")
        {
            User chiefRestorations = await GetUserByRoleName("Restoration Chief");
            //Sending email
            await SendEmail(chiefRestorations.Email, "Restorations Tasks", messageRestorationsChief);
        }

        //If the is info related to restoration that is needed to be informed, then send the corresponding email to the museum boss
        string messageMuseumBoss = await GetMessageForMuseumBoss(artworksMuseumLoanExpired, artworksFriendMuseumLoanExpired);
        if (messageMuseumBoss != "")
        {
            User museumBoss = await GetUserByRoleName("Museum Boss");
            //Sending email
            await SendEmail(museumBoss.Email, "Loans Tasks", messageMuseumBoss);
        }
    }

    public async Task SendEmail(string toEmail, string subject, string body)
    {
        //Get configs from appsettings.json
        string smtpServer = configuration.GetValue<string>("EmailConfiguration:SmtpServer");
        string fromEmail = configuration.GetValue<string>("EmailConfiguration:From");
        string password = configuration.GetValue<string>("EmailConfiguration:Password");
        int port = configuration.GetValue<int>("EmailConfiguration:Port");

        var smtpClient = new SmtpClient(smtpServer)
        {
            Port = port,
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true,
            UseDefaultCredentials = false,
            DeliveryMethod = SmtpDeliveryMethod.Network,
        };

        var message = new MailMessage(fromEmail, toEmail)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        try
        {
            smtpClient.Send(message);
            logger.LogInformation($"Email sent successfully to {toEmail}!");
        }
        catch (SmtpException ex)
        {
            logger.LogInformation($"Something went wrong!: {ex.ToString()}");
        }
    }

    public async Task<ICollection<Artwork>> GetRestorationNeededArtworks()
    {
        DbRepository<Artwork> repository = new DbArtworksRepository(this.serviceProvider);
        IEnumerable<Artwork> artworks = await repository.GetObjectsAsync();
        List<Artwork> artworksToRestoration = new List<Artwork>();

        foreach (Artwork artwork in artworks)
            if (artwork.IncorporatedDate != null && (artwork.CurrentStatus is Artwork.ArtworkStatus.OnDisplay || artwork.CurrentStatus is Artwork.ArtworkStatus.InStorage) && (await DaysSinceLastRestoration(artwork)) >= 5 * 365)
                artworksToRestoration.Add(artwork);

        return artworksToRestoration;
    }

    public async Task<int> DaysSinceLastRestoration(Artwork artwork)
    {
        DbRepository<Restoration> repository = new DbRestorationsRepository(this.serviceProvider);
        IEnumerable<Restoration> restorations = await repository.GetObjectsAsync();
        bool isInRestorations = false;
        DateTime? lastRestorationEndingDate = DateTime.MinValue;

        foreach (Restoration restoration in restorations)
        {
            if (restoration.Artwork.Id == artwork.Id)
            {
                if (!isInRestorations)
                    isInRestorations = true;
                if (restoration.EndDate != null && restoration.EndDate > lastRestorationEndingDate)
                    lastRestorationEndingDate = restoration.EndDate;
            }
        }

        if (!isInRestorations)
            return (DateTime.UtcNow.Date - Convert.ToDateTime(artwork.IncorporatedDate).Date).Days;
        return (DateTime.UtcNow.Date - Convert.ToDateTime(lastRestorationEndingDate).Date).Days;
    }

    public async Task<ICollection<Artwork>> GetMuseumLoanExpiredArtworks()
    {
        DbRepository<Loan> repository = new DbLoansRepository(this.serviceProvider);
        IEnumerable<Loan> loans = await repository.GetObjectsAsync();
        List<Artwork> artworksToFinishLoan = new List<Artwork>();

        foreach (Loan loan in loans)
            if ((DateTime.UtcNow.Date - loan.StartDate.Date).Days >= loan.Application.Duration && loan.Application.Artwork.CurrentStatus is Artwork.ArtworkStatus.OnLoan)
                artworksToFinishLoan.Add(loan.Application.Artwork);

        return artworksToFinishLoan;
    }

    public async Task<ICollection<Artwork>> GetFriendMuseumsLoanExpiredArtworks()
    {
        DbRepository<Loan> repository = new DbLoansRepository(this.serviceProvider);
        IEnumerable<Loan> loans = await repository.GetObjectsAsync();
        List<Artwork> artworksToFriendMuseum = new List<Artwork>();

        foreach (Loan loan in loans)
            if (loan.Application.Artwork.CurrentStatus == Artwork.ArtworkStatus.OnDisplay && (loan.Application.Artwork.Museum != null) && (DateTime.UtcNow.Date - loan.StartDate.Date).Days >= loan.Application.Duration && loan.Application.Artwork.CurrentStatus is Artwork.ArtworkStatus.OnLoan)
                artworksToFriendMuseum.Add(loan.Application.Artwork);

        return artworksToFriendMuseum;
    }

    public async Task<string> GetMessageForRestorationsChief(ICollection<Artwork> artworksNeedRestoration)
    {
        string message = "";

        if (artworksNeedRestoration.Count > 0)
        {
            message = "List of artworks that need to init restoration process:\n";
            foreach (Artwork artwork in artworksNeedRestoration)
                message = message + "Artwork with id: " + artwork.Id + " and title: " + artwork.Title + "\n";
            message = message + "\n";
        }

        return message;
    }

    public async Task<string> GetMessageForMuseumBoss(ICollection<Artwork> artworksMuseumLoanExpired, ICollection<Artwork> artworksFriendMuseumLoanExpired)
    {
        string message = "";

        if (artworksMuseumLoanExpired.Count > 0)
        {
            message = "List of artworks that finished its restoration process:\n";
            foreach (Artwork artwork in artworksMuseumLoanExpired)
                message = message + "Artwork with id: " + artwork.Id + " and title: " + artwork.Title + "\n";
            message = message + "\n";
        }

        if (artworksFriendMuseumLoanExpired.Count > 0)
        {
            message = "List of artworks that need to init restoration process:\n";
            foreach (Artwork artwork in artworksFriendMuseumLoanExpired)
                message = message + "Artwork with id: " + artwork.Id + " and title: " + artwork.Title + "\n";
            message = message + "\n";
        }

        return message;
    }

    public async Task<User> GetUserByRoleName(string roleUser)
    {
        DbRepository<User> repository = new DbUsersRepository(this.serviceProvider);
        IEnumerable<User> users = await repository.GetObjectsAsync();
        foreach (User user in users)
            if (user.Role.Name == roleUser)
                return user;
        return null;
    }
}