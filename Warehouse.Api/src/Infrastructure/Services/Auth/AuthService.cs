using Microsoft.EntityFrameworkCore;
using Warehouse.Api.src.Domain;
using Warehouse.Api.src.Domain.Entities;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Infrastructure.Providers.Services;
using Warehouse.Api.src.Persistence.UnitOfWork;

namespace Warehouse.Api.src.Infrastructure.Services.Auth;

public sealed class AuthService(IUnitOfWork unitOfWork, TokenService tokenService, BaseDbContext db) : IAuthService
{
    public async Task RegistrAsync(RegistrDto dto)
    {
        
        if (await db.Users.Where(u => u.Email == dto.Email.ToLower()).AnyAsync()) throw new Exception("User is already exists");

        try
        {
            db.Users.Add(new User
            {
                Id = Guid.CreateVersion7(),
                Fullname = dto.Fullname,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password, BCrypt.Net.BCrypt.GenerateSalt()),
                CreatedAt = DateTime.UtcNow
            });
            await unitOfWork.SaveAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new DbUpdateException(ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await db.Users.Where(u => u.Email == dto.Email.ToLower()).FirstOrDefaultAsync();
        if (user is null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            throw new Exception("Email/Password is incorrect");

        var token = tokenService.GenerateToken(user);
        return new LoginResponseDto(token);
    }
    
    public async Task ChangePassword(ChangePasswordDto dto)
    {
        var user = await unitOfWork.AuthRepository.Get(u => u.Email == dto.Email.ToLower()) ?? throw new ArgumentException("User not found");
    
        if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, user.Password)) throw new ArgumentException("Old password is incorrect");

        try
        {
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword, BCrypt.Net.BCrypt.GenerateSalt());
            await unitOfWork.SaveAsync();
        }
        catch (DbUpdateException e)
        {
            throw new DbUpdateException(e.Message);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}