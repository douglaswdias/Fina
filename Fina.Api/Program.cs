using System.Data.Common;
using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

const string connectionString =
    "server=localhost\\sqlexpress;database=Fina;trusted_connection=True;TrustServerCertificate=True";

builder.Services.AddDbContext<AppDbContext>(
    x => x.UseSqlServer(connectionString)
);

builder.Services.AddTransient<ICategoryHandler, CategoryHandler>();
builder.Services.AddTransient<ITransactionHandler, TransactionsHandler>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
