namespace Disney_Characters.Extensions;

public static class SwaggerExtension
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
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
    }
}