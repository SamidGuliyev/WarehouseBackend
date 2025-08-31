using Warehouse.Api.src.Infrastructure.DTOs;

namespace Warehouse.Api.src.Infrastructure.Services.Auth;

public interface IAuthService
{
    Task RegistrAsync(RegistrDto dto);
    Task<LoginResponseDto> LoginAsync(LoginDto dto);
    Task ChangePassword(ChangePasswordDto dto);
}