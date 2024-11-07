using Bank.Api.Data;
using Bank.Api.Endpoints;
using Bank.Api.Handlers;
using Bank.Core.Handlers;
using Bank.Core.Models;
using Bank.Core.Requests.Categories;
using Bank.Core.Responses;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var cnnStr = builder
    .Configuration
    .GetConnectionString("DefaultConnection") ?? string.Empty;


builder.Services.AddDbContext<AppDbContext>(
  x=>
  {
    x.UseSqlServer(cnnStr);
  }
);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
  x.CustomSchemaIds(n => n.FullName); //full qualified name
});

builder.Services.AddTransient<ICategoryHandler,CategoryHandler>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => new {message = "ok"});
app.MapEndpoints();

app.Run();


