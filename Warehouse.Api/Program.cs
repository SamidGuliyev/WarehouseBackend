using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Infrastructure.Services;
using Warehouse.Api.src.Persistence.UnitOfWork;
using Warehouse.Api.Features;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<BaseDbContext>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddInfrastructureScopeResolvers();
builder.Services.AddPresentationScopeResolvers(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

if (app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();


