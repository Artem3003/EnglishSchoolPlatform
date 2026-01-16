using Auth.Application.DTOs;
using Auth.Application.Interfaces;
using Auth.Domain.Data;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Auth.Application.Services;

/// <summary>
/// Service for authentication operations.
/// </summary>
/// <param name="userManager">The user manager.</param>
/// <param name="signInManager">The sign-in manager.</param>
/// <param name="tokenService">The token service.</param>
/// <param name="context">The database context.</param>
/// <param name="logger">The logger.</param>
public class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    ITokenService tokenService,
    AuthDbContext context,
    ILogger<AuthService> logger) : IAuthService
{
    /// <inheritdoc />
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequestDto request, string? ipAddress)
    {
        logger.LogInformation("Registration attempt for email: {Email}", request.Email);

        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            logger.LogWarning("Registration failed: email {Email} already exists", request.Email);
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "A user with this email already exists.",
            };
        }

        if (request.Password != request.ConfirmPassword)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "Passwords do not match.",
            };
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            CreatedAt = DateTime.UtcNow,
        };

        var result = await userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogWarning("Registration failed for {Email}: {Errors}", request.Email, errors);
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = errors,
            };
        }

        // Assign default role
        await userManager.AddToRoleAsync(user, "Student");

        logger.LogInformation("User {Email} registered successfully", request.Email);

        // Generate tokens
        var roles = await userManager.GetRolesAsync(user);
        var (accessToken, expiration) = tokenService.GenerateAccessToken(user, roles);
        var refreshToken = tokenService.GenerateRefreshToken(ipAddress);

        user.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();

        return new AuthResponseDto
        {
            IsSuccess = true,
            UserId = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            AccessTokenExpiration = expiration,
            Roles = roles,
        };
    }

    /// <inheritdoc />
    public async Task<AuthResponseDto> LoginAsync(LoginRequestDto request, string? ipAddress)
    {
        logger.LogInformation("Login attempt for email: {Email}", request.Email);

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            logger.LogWarning("Login failed: user {Email} not found", request.Email);
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "Invalid email or password.",
            };
        }

        if (!user.IsActive)
        {
            logger.LogWarning("Login failed: user {Email} is deactivated", request.Email);
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "Your account has been deactivated. Please contact support.",
            };
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: true);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                logger.LogWarning("Login failed: user {Email} is locked out", request.Email);
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    ErrorMessage = "Your account has been locked due to multiple failed login attempts. Please try again later.",
                };
            }

            logger.LogWarning("Login failed: invalid password for {Email}", request.Email);
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "Invalid email or password.",
            };
        }

        // Generate tokens
        var roles = await userManager.GetRolesAsync(user);
        var (accessToken, expiration) = tokenService.GenerateAccessToken(user, roles);
        var refreshToken = tokenService.GenerateRefreshToken(ipAddress);

        // Remove old refresh tokens
        await RemoveOldRefreshTokensAsync(user.Id);

        user.RefreshTokens.Add(refreshToken);
        user.UpdatedAt = DateTime.UtcNow;
        await context.SaveChangesAsync();

        logger.LogInformation("User {Email} logged in successfully", request.Email);

        return new AuthResponseDto
        {
            IsSuccess = true,
            UserId = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            AccessTokenExpiration = expiration,
            Roles = roles,
        };
    }

    /// <inheritdoc />
    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request, string? ipAddress)
    {
        var userId = tokenService.GetUserIdFromToken(request.AccessToken);
        if (userId == null)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "Invalid access token.",
            };
        }

        var user = await context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "User not found.",
            };
        }

        var refreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == request.RefreshToken);
        if (refreshToken == null)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "Invalid refresh token.",
            };
        }

        if (!refreshToken.IsActive)
        {
            return new AuthResponseDto
            {
                IsSuccess = false,
                ErrorMessage = "Refresh token is expired or revoked.",
            };
        }

        // Revoke old refresh token
        refreshToken.RevokedAt = DateTime.UtcNow;
        refreshToken.RevokedByIp = ipAddress;
        refreshToken.ReasonRevoked = "Replaced by new token";

        // Generate new tokens
        var roles = await userManager.GetRolesAsync(user);
        var (accessToken, expiration) = tokenService.GenerateAccessToken(user, roles);
        var newRefreshToken = tokenService.GenerateRefreshToken(ipAddress);

        refreshToken.ReplacedByToken = newRefreshToken.Token;
        user.RefreshTokens.Add(newRefreshToken);

        await context.SaveChangesAsync();

        logger.LogInformation("Token refreshed for user {UserId}", user.Id);

        return new AuthResponseDto
        {
            IsSuccess = true,
            UserId = user.Id,
            Email = user.Email!,
            FirstName = user.FirstName,
            LastName = user.LastName,
            AccessToken = accessToken,
            RefreshToken = newRefreshToken.Token,
            AccessTokenExpiration = expiration,
            Roles = roles,
        };
    }

    /// <inheritdoc />
    public async Task<bool> RevokeTokenAsync(string refreshToken, string? ipAddress)
    {
        var token = await context.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshToken);

        if (token == null || !token.IsActive)
        {
            return false;
        }

        token.RevokedAt = DateTime.UtcNow;
        token.RevokedByIp = ipAddress;
        token.ReasonRevoked = "Revoked by user";

        await context.SaveChangesAsync();

        logger.LogInformation("Refresh token revoked for user {UserId}", token.UserId);

        return true;
    }

    /// <inheritdoc />
    public async Task<bool> LogoutAsync(Guid userId, string? ipAddress)
    {
        var user = await context.Users
            .Include(u => u.RefreshTokens)
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        foreach (var token in user.RefreshTokens.Where(rt => rt.IsActive))
        {
            token.RevokedAt = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = "Logged out";
        }

        await context.SaveChangesAsync();

        logger.LogInformation("User {UserId} logged out", userId);

        return true;
    }

    private async Task RemoveOldRefreshTokensAsync(Guid userId)
    {
        var oldTokens = await context.RefreshTokens
            .Where(rt => rt.UserId == userId && !rt.IsRevoked && rt.ExpiresAt <= DateTime.UtcNow)
            .ToListAsync();

        context.RefreshTokens.RemoveRange(oldTokens);
    }
}
