using Azure.Identity;
using Tarker.Booking.Api;
using Tarker.Booking.Application;
using Tarker.Booking.Common;
using Tarker.Booking.External;
using Tarker.Booking.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
    .AddWebApi()
    .AddCommon()
    .AddApplication()
    .AddExternal(builder.Configuration)
    .AddPersistence(builder.Configuration);

builder.Services.AddControllers();

// KeyVoult
var KeyVoultUrl = builder.Configuration["KeyVoultUrl"] ?? string.Empty;

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "local")
{
    string tenantId = Environment.GetEnvironmentVariable("tenantId") ?? string.Empty;
    string clientId = Environment.GetEnvironmentVariable("clientId") ?? string.Empty;
    string clientSecret = Environment.GetEnvironmentVariable("clientSecret") ?? string.Empty;

    var tokenCredential = new ClientSecretCredential(tenantId, clientId, clientSecret);

    builder.Configuration.AddAzureKeyVault(new Uri(KeyVoultUrl), tokenCredential);
}
else
{

    builder.Configuration.AddAzureKeyVault(new Uri(KeyVoultUrl), new DefaultAzureCredential());
}

// Prueba de obtención de un secreto
var sql = builder.Configuration["SQLConnectionString"];


var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Run();