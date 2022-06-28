using Microsoft.OpenApi.Models;
using IMuseum.Business;
using IMuseum.Persistence.Services;
using IMuseum.Auth.Authorization;
using Quartz;
using IMuseum.Scheduling;

var builder = WebApplication.CreateBuilder(args);

// add services to DI container
{
    var services = builder.Services;
    services.AddCors();
    services.AddControllers();

    // configure DI for application services
    services.AddScoped<IUserService, UserService>();
}

//Add sublayers dependencies
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddBusinessControllers();

//Add API services 
builder.Services.AddControllers(options => { options.SuppressAsyncSuffixInActionNames = false; });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "iMuseum API", Version = "v1" });
});


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors();

var app = builder.Build();

// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    // custom basic auth middleware
    app.UseMiddleware<BasicAuthMiddleware>();

    app.MapControllers();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");
});

app.MapControllerRoute(
    name: "default",
    pattern: "/api/{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

//Adding service for scheduler
IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            q.ScheduleJob<SendMailJob>(trigger => trigger
                .WithIdentity("SendRecurringMailTrigger")
                .WithSimpleSchedule(s =>
                    s.WithIntervalInHours(20)
                    .RepeatForever()
                )
                .WithDescription("This trigger will run every 24 hour to send emails if necessary.")
            );
        });

        services.AddQuartzHostedService(options =>
        {
            // when shutting down we want jobs to complete gracefully
            options.WaitForJobsToComplete = true;
        });
    })
    .Build();

host.RunAsync();

app.Run();

