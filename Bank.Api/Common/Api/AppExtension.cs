using System.Security.Claims;
using Bank.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Bank.Api.Common.Api;

public static class AppExtension
{
  public static void ConfigureDevEnvironment(this WebApplication app)
  {
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapSwagger().RequireAuthorization();
  }

  public static void UseSecurity(this WebApplication app)
  {
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapGroup("v1/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

    app.MapGroup("v1/identity")
        .WithTags("Identity")
        .MapPost("/logout", async (SignInManager<User> singInManager) =>
        {
          await singInManager.SignOutAsync();
          return Results.Ok();
        })
        .RequireAuthorization();

    app.MapGroup("v1/identity")
        .WithTags("Identity")
        .MapGet("/roles", (ClaimsPrincipal user) =>
        {
          if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Results.Unauthorized();

          var identity = (ClaimsIdentity)user.Identity;
          var roles = identity
      .FindAll(identity.RoleClaimType)
      .Select(c => new
            {
              c.Issuer,
              c.OriginalIssuer,
              c.Type,
              c.Value,
              c.ValueType

            });


          return TypedResults.Json(roles);
        })
        .RequireAuthorization();

  }
}