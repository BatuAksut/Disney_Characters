using Disney_Characters.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddDisneyHttpClient();

// Yeni eklenen database konfigürasyonu
builder.Services.AddDatabaseConfiguration(builder.Configuration);

var app = builder.Build();

try
{
    Log.Information("Application is starting");

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Database initialization
    await app.InitializeDatabaseAsync();

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application failed to start");
}
finally
{
    Log.CloseAndFlush();
}