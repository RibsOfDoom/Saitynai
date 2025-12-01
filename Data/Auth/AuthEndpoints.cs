using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using L1_Zvejyba.Data.Auth.Model;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace L1_Zvejyba.Data.Auth
{
    public static class AuthEndpoints
    {
        public static void _AddAuthApi(this WebApplication app)
        {
            //register
            app.MapPost("api/accounts", async (UserManager<User> userManager, RegisterUserDTO dto) =>
            {
                ///check user existance
                ///currently just checks if name not null
                var user = await userManager.FindByNameAsync(dto.UserName);
                if (user != null)
                    return Results.UnprocessableEntity("user != null");

                var newUser = new User()
                {
                    Email = dto.Email,
                    UserName = dto.UserName,
                };


                //TODO: warp in transaction
                var createUserResult = await userManager.CreateAsync(newUser, dto.Password);
                if (!createUserResult.Succeeded)
                    return Results.UnprocessableEntity("Something went wrong.");

                await userManager.AddToRoleAsync(newUser, UserRoles.User);

                return Results.Created();
            });

            //login
            app.MapPost("api/login", async (UserManager<User> userManager, LoginDTO dto, JwtTokenService jwtTokenService, HttpContext httpContext, SessionService sessionService) =>
            {
                ///check user existance
                var user = await userManager.FindByNameAsync(dto.UserName);
                if (user == null)
                    return Results.UnprocessableEntity("User with such name does not exist");

                var isPasswordValid = await userManager.CheckPasswordAsync(user, dto.Password);
                if (!isPasswordValid)
                    return Results.UnprocessableEntity("Incorrect username or password. Try again.");

                var roles = await userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    await userManager.AddToRoleAsync(user, role);
                }

                //var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

                var sessionId = Guid.NewGuid();
                var expiresAt = DateTime.UtcNow.AddDays(3);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(sessionId, user.Id, expiresAt);

                await sessionService.CreateSessionAsync(sessionId, user.Id, refreshToken, expiresAt);



                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = expiresAt,
                    //Secure = false => maybe true
                };

                httpContext.Response.Cookies.Append("RefreshToken", refreshToken, cookieOptions);

                return Results.Ok(new SuccessfulLoginDTO(accessToken));
            });

            ///Access token refresh I think

            app.MapPost("api/accessToken", async (UserManager<User> userManager, JwtTokenService jwtTokenService, HttpContext httpContext, SessionService sessionService) =>
            {
                if (!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Results.UnprocessableEntity("Failed to find cookie.");
                }

                if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                {
                    return Results.UnprocessableEntity("Your cookies were baked poorly.");
                }

                var sessionId = claims.FindFirstValue("SessionId");
                if (string.IsNullOrWhiteSpace(sessionId))
                {
                    return Results.UnprocessableEntity("SessionId is invalid.");
                }

                var sessionIdAsGuid = Guid.Parse(sessionId);
                if (!await sessionService.IsSessionValidAsync(sessionIdAsGuid, refreshToken))
                {
                    return Results.UnprocessableEntity("Session expired.");
                }

                var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);
                var user = await userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return Results.UnprocessableEntity("User not found.");
                }

                var roles = await userManager.GetRolesAsync(user);

                //var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);

                var expiresAt = DateTime.UtcNow.AddDays(3);
                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var newRefreshToken = jwtTokenService.CreateRefreshToken(sessionIdAsGuid, user.Id, expiresAt);

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = expiresAt,
                    //Secure = false => maybe true
                };

                httpContext.Response.Cookies.Append("RefreshToken", newRefreshToken, cookieOptions);

                await sessionService.ExtendedSessionAsync(sessionIdAsGuid, newRefreshToken, expiresAt);

                return Results.Ok(new SuccessfulLoginDTO(accessToken));
            });

            app.MapPost("api/logout", async (UserManager<User> userManager, JwtTokenService jwtTokenService, HttpContext httpContext, SessionService sessionService) =>
            {
                if (!httpContext.Request.Cookies.TryGetValue("RefreshToken", out var refreshToken))
                {
                    return Results.UnprocessableEntity("Failed to find cookie.");
                }

                if (!jwtTokenService.TryParseRefreshToken(refreshToken, out var claims))
                {
                    return Results.UnprocessableEntity("Your cookies were baked poorly.");
                }

                var sessionId = claims.FindFirstValue("SessionId");
                if (string.IsNullOrWhiteSpace(sessionId))
                {
                    return Results.UnprocessableEntity("SessionId is invalid.");
                }

                var sessionIdAsGuid = Guid.Parse(sessionId);
                if (!await sessionService.IsSessionValidAsync(sessionIdAsGuid, refreshToken))
                {
                    return Results.UnprocessableEntity("Session expired.");
                }

                await sessionService.InvalidateSessionAsync(Guid.Parse(sessionId));
                httpContext.Response.Cookies.Delete("RefreshToken");


                return Results.Ok("Logged out.");
            });

        }


        public record RegisterUserDTO(string UserName, string Email, string Password);
        public record LoginDTO(string UserName, string Password);
        public record SuccessfulLoginDTO(string AccessToken);
    }
}
