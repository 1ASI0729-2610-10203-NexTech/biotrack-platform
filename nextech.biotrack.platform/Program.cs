using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.CorporateManagement.Application.CommandServices;
using nextech.biotrack.platform.CorporateManagement.Application.Internal.CommandServices;
using nextech.biotrack.platform.CorporateManagement.Application.Internal.QueryServices;
using nextech.biotrack.platform.CorporateManagement.Application.QueryServices;
using nextech.biotrack.platform.CorporateManagement.Domain.Repositories;
using nextech.biotrack.platform.CorporateManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.Iam.Application.Acl;
using nextech.biotrack.platform.Iam.Application.CommandServices;
using nextech.biotrack.platform.Iam.Application.Internal.CommandServices;
using nextech.biotrack.platform.Iam.Application.Internal.OutboundServices;
using nextech.biotrack.platform.Iam.Application.Internal.QueryServices;
using nextech.biotrack.platform.Iam.Application.QueryServices;
using nextech.biotrack.platform.Iam.Domain.Repositories;
using nextech.biotrack.platform.Iam.Infrastructure.Hashing.BCrypt.Services;
using nextech.biotrack.platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.Iam.Infrastructure.Tokens.Jwt.Configuration;
using nextech.biotrack.platform.Iam.Infrastructure.Tokens.Jwt.Services;
using nextech.biotrack.platform.Iam.Interfaces.Acl;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.PatientProfile.Application.CommandServices;
using nextech.biotrack.platform.PatientProfile.Application.Internal.CommandServices;
using nextech.biotrack.platform.PatientProfile.Application.Internal.QueryServices;
using nextech.biotrack.platform.PatientProfile.Application.QueryServices;
using nextech.biotrack.platform.PatientProfile.Domain.Repositories;
using nextech.biotrack.platform.PatientProfile.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.ProgressTracking.Application.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Application.Internal.CommandServices;
using nextech.biotrack.platform.ProgressTracking.Application.Internal.QueryServices;
using nextech.biotrack.platform.ProgressTracking.Application.QueryServices;
using nextech.biotrack.platform.ProgressTracking.Domain.Repositories;
using nextech.biotrack.platform.ProgressTracking.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.Shared.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;
using nextech.biotrack.platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using nextech.biotrack.platform.SubscriptionsBilling.Application.CommandServices;
using nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.CommandServices;
using nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.OutboundServices;
using nextech.biotrack.platform.SubscriptionsBilling.Application.Internal.QueryServices;
using nextech.biotrack.platform.SubscriptionsBilling.Application.QueryServices;
using nextech.biotrack.platform.SubscriptionsBilling.Domain.Repositories;
using nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.SubscriptionsBilling.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseNpgsql(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Nextech BioTrack Platform",
        Version = "v1",
        Description = "BioTrack Platform API - AV2"
    });
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Please enter JWT token",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.EnableAnnotations();
});

// Shared
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Corporate Management
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyCommandService, CompanyCommandService>();
builder.Services.AddScoped<ICompanyQueryService, CompanyQueryService>();

// IAM
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// Nutritional Planning
builder.Services.AddScoped<INutritionalPlanRepository, NutritionalPlanRepository>();
builder.Services.AddScoped<INutritionalPlanCommandService, NutritionalPlanCommandService>();
builder.Services.AddScoped<INutritionalPlanQueryService, NutritionalPlanQueryService>();

// Patient Profile
builder.Services.AddScoped<IHealthProfileRepository, HealthProfileRepository>();
builder.Services.AddScoped<IHealthProfileCommandService, HealthProfileCommandService>();
builder.Services.AddScoped<IHealthProfileQueryService, HealthProfileQueryService>();

// Progress Tracking
builder.Services.AddScoped<IWeightRecordRepository, WeightRecordRepository>();
builder.Services.AddScoped<IFoodEntryRepository, FoodEntryRepository>();
builder.Services.AddScoped<IActivityEntryRepository, ActivityEntryRepository>();
builder.Services.AddScoped<IProgressCommandService, ProgressCommandService>();
builder.Services.AddScoped<IProgressQueryService, ProgressQueryService>();

// Subscriptions & Billing
builder.Services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
builder.Services.AddScoped<ICorporateSubscriptionRepository, CorporateSubscriptionRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<IActivateSubscriptionCommandService, ActivateSubscriptionCommandService>();
builder.Services.AddScoped<IProcessRenewalCommandService, ProcessRenewalCommandService>();
builder.Services.AddScoped<ISuspendSubscriptionCommandService, SuspendSubscriptionCommandService>();
builder.Services.AddScoped<IReactivateSubscriptionCommandService, ReactivateSubscriptionCommandService>();
builder.Services.AddScoped<IBillingSummaryQueryService, BillingSummaryQueryService>();
builder.Services.AddScoped<IPaymentGatewayAdapter, PaymentGatewayAdapter>();

// Mediator
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator([typeof(Program)]);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.EnsureCreated();

        if (!context.Set<nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities.SubscriptionPlan>().Any())
        {
            context.Set<nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities.SubscriptionPlan>().AddRange(
                new nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities.SubscriptionPlan(
                    "Basic Individual",
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.PlanType.Individual,
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.BillingCycle.Monthly, 9.99m),
                new nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities.SubscriptionPlan(
                    "Premium Individual",
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.PlanType.Individual,
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.BillingCycle.Monthly, 19.99m),
                new nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities.SubscriptionPlan(
                    "Corporate Plan",
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.PlanType.Corporate,
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.BillingCycle.Monthly, 49.99m),
                new nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.Entities.SubscriptionPlan(
                    "Free Trial",
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.PlanType.Trial,
                    nextech.biotrack.platform.SubscriptionsBilling.Domain.Model.ValueObjects.BillingCycle.Monthly, 0m)
            );
            context.SaveChanges();
        }
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogWarning("Could not connect to database on startup: {Message}", ex.Message);
    }
}

app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseRequestAuthorization();
app.UseAuthorization();
app.MapControllers();

app.Run();
