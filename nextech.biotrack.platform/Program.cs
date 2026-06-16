using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using nextech.biotrack.platform.NutritionalPlanning.Application.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.CommandServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.OutboundServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.Internal.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Application.QueryServices;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Repositories;
using nextech.biotrack.platform.NutritionalPlanning.Domain.Services;
using nextech.biotrack.platform.NutritionalPlanning.Infrastructure.OutboundServices;
using nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using nextech.biotrack.platform.NutritionalPlanning.Infrastructure.Services;
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

    options.UseMySQL(connectionString)
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
});

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Nutritional Planning Bounded Context
builder.Services.AddScoped<IInitialEvaluationRepository, InitialEvaluationRepository>();
builder.Services.AddScoped<INutritionalPlanRepository, NutritionalPlanRepository>();
builder.Services.AddScoped<IControlAppointmentRepository, ControlAppointmentRepository>();
builder.Services.AddScoped<IInitialEvaluationCommandService, InitialEvaluationCommandService>();
builder.Services.AddScoped<INutritionalPlanCommandService, NutritionalPlanCommandService>();
builder.Services.AddScoped<IControlAppointmentCommandService, ControlAppointmentCommandService>();
builder.Services.AddScoped<INutritionalPlanQueryService, NutritionalPlanQueryService>();
builder.Services.AddScoped<IEmailAdapter, EmailAdapter>();
builder.Services.AddScoped<INutritionistAssignmentService, NutritionistAssignmentService>();

// IAM Bounded Context
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<IIamContextFacade, IamContextFacade>();

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
