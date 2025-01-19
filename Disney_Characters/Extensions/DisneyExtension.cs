using Core.DataAccess.EntityFramework;
using DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Disney_Characters.Models;
using Microsoft.EntityFrameworkCore;

namespace Disney_Characters.Extensions;

public static class DisneyExtension
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
   

        public static void AddCharacterRepository(this IServiceCollection services)
        {
         services.AddScoped<ICharacterRepository, CharacterRepository>();

    }



}