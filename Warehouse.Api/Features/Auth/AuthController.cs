using System.Security.Claims;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warehouse.Api.src.Infrastructure.DTOs;
using Warehouse.Api.src.Infrastructure.Services.Auth;

namespace Warehouse.Api.Features.Auth;

[Route("api/auth"), ApiController]
public sealed class AuthController(
    IAuthService authService,
    IValidator<RegistrDto> validatorRegistr,
    IValidator<LoginDto> validatorLogin,
    IValidator<ChangePasswordDto> validatorChangePassword
) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Registr(RegistrDto dto)
    {
        var registrValidation = await validatorRegistr.ValidateAsync(dto);
        if (!registrValidation.IsValid) return BadRequest(registrValidation.Errors);
        await authService.RegistrAsync(dto);

        return Created();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var loginValidation = await validatorLogin.ValidateAsync(dto);
        if (!loginValidation.IsValid) return BadRequest(loginValidation.Errors);
        var jwtToken = await authService.LoginAsync(dto);
        return Ok(jwtToken);
    }

    [HttpPost("logout"), Authorize]
    public IActionResult Logout(LogoutDto dto)
    {
        if (!User.HasClaim(x => x.Type == ClaimTypes.Email)
            || User.FindFirstValue(ClaimTypes.Email) is not { } email
            || !email.Equals(dto.Email, StringComparison.InvariantCultureIgnoreCase)) return Forbid();

        HttpContext.User = new ClaimsPrincipal();
        return NoContent();
    }
    [HttpPost("change-password"), Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        var validator = await validatorChangePassword.ValidateAsync(dto);
        if (!validator.IsValid) return BadRequest(validator.Errors);

        await authService.ChangePassword(dto);
        return NoContent();
    }
}