using Bank.Api.Data;
using Bank.Api.Endpoints;
using Bank.Api.Handlers;
using Bank.Api.Models;
using Bank.Core.Handlers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x => { x.CustomSchemaIds(n => n.FullName); });

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();
builder.Services.AddAuthorization();

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;


builder
  .Services
  .AddDbContext<AppDbContext>(
                x => { x.UseSqlServer(cnnStr); });
builder.Services
.AddIdentityCore<User>()
.AddRoles<IdentityRole<long>>()
.AddEntityFrameworkStores<AppDbContext>()
.AddApiEndpoints();

builder
.Services
.AddTransient<ICategoryHandler, CategoryHandler>();

builder
.Services
.AddTransient<ITransactionHandler, TransactionHandler>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => new { message = "ok" });
app.MapEndpoints();
app.MapGroup("v1/identity")
.WithTags("Identity")
.MapIdentityApi<User>();

app.Run();


