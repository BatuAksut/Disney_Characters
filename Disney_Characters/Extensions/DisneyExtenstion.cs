namespace Disney_Characters.Extensions;

public static class DisneyExtenstion
{
    public static void AddDisneyHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("DisneyApi", client =>
        {
            client.BaseAddress = new Uri("https://api.disneyapi.dev/");
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });
        services.AddScoped<IDisneyCharacterService, DisneyCharacterService>();
    }
}