using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain;

namespace Warehouse.Api.Application.BackgroundServices;

public sealed class TemporaryFileCleaningBackgroundService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken cancellation)
    {
        using var timer = new PeriodicTimer(TimeSpan.FromHours(8));
        
        while (await timer.WaitForNextTickAsync(cancellation))
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
            var productDbImg = db.Products.Where(p => p.Thumbnail != null).ToDictionary(p => p.Thumbnail,  p => p);
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "products");
            var productFolderImg = Directory.GetFiles(path);

            foreach (var result in productFolderImg)
            {
                if (!productDbImg.ContainsKey(result.Split(@"wwwroot\")?[1])) File.Delete(result);
            }
            
            
            foreach (var filePath in files)
            {
                if (string.IsNullOrEmpty(filePath)) continue;
                var fileFullPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", filePath);
                var fileName = filePath[(filePath.LastIndexOf('\\') + 1)..];
                // if (!await db.Products.Where(p => p.Thumbnail != null && p.Thumbnail.Contains(fileName)).AnyAsync(cancellationToken: cancellation)) 
                if (!await db.Products.AnyAsync(p => p.Thumbnail == filePath, cancellationToken: cancellation))
                    File.Delete(fileFullPath);
                else
                {
                    var productPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", "uploads","products");
                    if(!Directory.Exists(productPath)) Directory.CreateDirectory(productPath);
                    File.Move(fileFullPath, Path.Combine(productPath, fileName));
                }
            }
        }
    }
}