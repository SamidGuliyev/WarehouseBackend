using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Bogus;
using Warehouse.Api.src.Domain.Entities;

namespace Warehouse.Api.src.Domain;

public sealed class BaseDbContext(DbContextOptions<BaseDbContext> option, IConfiguration config) : DbContext(option)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(config.GetConnectionString("Postgre"));
        optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning)); // errordan qacmaq ucun.
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductColor> ProductColors { get; set; }
    public DbSet<ProductModelName> ProductModelNames { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(Program).Assembly);

        var userId = Guid.NewGuid();

        builder.Entity<User>().HasData([
            new User {
                Id = userId,
                Email = config.GetValue<string>("AdminSecrets:Email")!,
                Fullname = config.GetValue<string>("AdminSecrets:Fullname")!,
                Password = BCrypt.Net.BCrypt.
                    HashPassword(config.GetValue<string>("AdminSecrets:Password")!, BCrypt.Net.BCrypt.
                        GenerateSalt()),
            }
        ]);

        builder.Entity<Product>(data =>
        {
            var id = 1;

            var fakerData = new Faker<Product>()
            .RuleFor(p => p.Id, f => id++)
            .RuleFor(p => p.Size, f => f.PickRandom("96", "128", "160", "192"))
            .RuleFor(p => p.Price, f => (float)Math.Round(f.Random.Float(0, 15), 2))
            .RuleFor(p => p.Thumbnail, f => f.Image.LoremFlickrUrl())
            .RuleFor(p => p.Stock, f => f.Random.Int(0, 100))
            .RuleFor(p => p.BlockNumber, f => f.Random.Int(1, 20))
            .RuleFor(p => p.PieceNumber, f => f.Random.Int(1, 1000))
            .RuleFor(p => p.ColorId, f => f.Random.Int(1, 3))
            .RuleFor(p => p.ProductModelId, f => f.Random.Int(1, 2))
            .RuleFor(p => p.UserId, userId)
            .Generate(50);

            data.HasData(fakerData);
        });

    }
}
