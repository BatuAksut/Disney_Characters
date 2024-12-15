var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Disney Characters API",
        Version = "v1",
        Description = "API for fetching Disney characters from an external API",
    });

    
    var xmlFile = Path.Combine(AppContext.BaseDirectory, "Disney_Characters.xml");
    c.IncludeXmlComments(xmlFile);
});




builder.Services.AddHttpClient("DisneyApi", client =>
{
    client.BaseAddress = new Uri("https://api.disneyapi.dev/");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
