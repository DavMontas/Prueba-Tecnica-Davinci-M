using Microsoft.OpenApi.Models;

namespace API.Extension
{
    public static class SwaggerConfig
    {
        public static void LoadSwagConfig(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Roulette API",
                    Description = "API for the Roulette game"
                });
                // Optional but helpful to avoid schema id collisions:
                c.CustomSchemaIds(t => t.FullName);
            });
        }
    }
}
