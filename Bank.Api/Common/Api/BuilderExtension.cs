using Bank.Api.Data;
using Bank.Api.Handlers;
using Bank.Api.Models;
using Bank.Core;
using Bank.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bank.Api.Common.Api;

public static class BuilderExtension
{
  public static void AddConfiguration(this WebApplicationBuilder builder)
  {

    Configuration.ConnectionString =
       builder
        .Configuration
        .GetConnectionString("DefaultConnection")
      ?? string.Empty;

  }

  public static void AddDocumentation(this WebApplicationBuilder builder)
  {

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });

  }

  public static void AddSecurity(this WebApplicationBuilder builder)
  {

    builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

    builder.Services.AddAuthorization();

  }

  public static void AddDataContexts(this WebApplicationBuilder builder)
  {

    builder
  .Services
  .AddDbContext<AppDbContext>(x => { x.UseSqlServer(Configuration.ConnectionString); });

    builder.Services
        .AddIdentityCore<User>()
        .AddRoles<IdentityRole<long>>()
        .AddEntityFrameworkStores<AppDbContext>()
        .AddApiEndpoints();

  }

  public static void AddCrossOrigin(this WebApplicationBuilder builder)
  {

  }

  public static void AddServices(this WebApplicationBuilder builder)
  {

    builder
      .Services
      .AddTransient<ICategoryHandler, CategoryHandler>();

    builder
      .Services
      .AddTransient<ITransactionHandler, TransactionHandler>();

  }


}

