using CW7_S30916.Repositories;
using CW7_S30916.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<ITripsRepository, TripsRepository>();
builder.Services.AddScoped<IClientTripRepository, ClientTripRepository>();

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ITripsService, TripsService>();
builder.Services.AddScoped<IClientTripService, ClientTripService>();



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();