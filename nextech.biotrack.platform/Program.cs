using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
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
using nextech.biotrack.platform.Shared.Domain.Repositories;
using nextech.biotrack.platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using nextech.biotrack.platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
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

// Add services to the container.
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()));
builder.Services.AddProblemDetails();

// Add CORS Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// Add Database Connection
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Nextech BioTrack Platform",
        Version = "v1",
        Description = "BioTrack Platform API"
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
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath)) options.IncludeXmlComments(xmlPath);
});

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// IAM Bounded Context
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

// SubscriptionsBilling Bounded Context
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

// Mediator Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator([typeof(Program)]);

var app = builder.Build();

// Apply pending migrations on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseGlobalExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
