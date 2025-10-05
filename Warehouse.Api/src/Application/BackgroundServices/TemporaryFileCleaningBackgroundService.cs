using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain;

namespace Warehouse.Api.Application.BackgroundServices;

public sealed class TemporaryFileCleaningBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

        do
        {
            
            var productsFileFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "temporary");
            IEnumerable<string> files;
            try
            {
                files = Directory.GetFiles(productsFileFolder).Select(f => f.Split(@"wwwroot\")?[1] ?? "");
            }
            catch (Exception)
            {
                continue;
            }
            
            await using var db = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<BaseDbContext>();
            
            foreach (var filePath in files)
            {
                if (string.IsNullOrEmpty(filePath)) continue;

                // var a = db.Products.FirstOrDefault(p => p.Thumbnail == filePath)?.Thumbnail;
                // Console.WriteLine(a); // burada gelen melumat uploads/temporary oldugu ucun berabar olmur db-da uploads/products olur
                var fileFullPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", filePath);
                var fileName = filePath[(filePath.LastIndexOf('\\') + 1)..];
                if (!await db.Products.Where(p => p.Thumbnail != null && p.Thumbnail.Contains(fileName)).AnyAsync(cancellationToken: cancellation)) 
                    File.Delete(fileFullPath);
                else
                {
                    var productPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "uploads","products");
                    if(!Directory.Exists(productPath)) Directory.CreateDirectory(productPath);
                    File.Move(fileFullPath, Path.Combine(productPath, fileName));
                }
            }
        }
        while (await timer.WaitForNextTickAsync(cancellation));
    }
}