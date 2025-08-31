namespace Warehouse.Api.src.Infrastructure.DTOs;

public record struct RegistrDto(string Fullname, string Email, string Password);

public record struct LoginDto(string Email, string Password);

public record struct LoginResponseDto(string Token);

public record struct LogoutDto(string Email);

public record struct ChangePasswordDto(string Email, string OldPassword, string NewPassword);
