using Bank.Api.Common.Api;
using Bank.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Bank.Api.Endpoints.Identity;

public class LogoutEndpoint : IEndpoint
{
  public static void Map(IEndpointRouteBuilder app)
 => app.MapPost("/logout", HandleAsync)
 .RequireAuthorization();

  private static async Task<IResult> HandleAsync(SignInManager<User> singInManager)
  {
    await singInManager.SignOutAsync();
    return Results.Ok();
  }

}