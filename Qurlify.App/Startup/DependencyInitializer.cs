using Microsoft.OpenApi.Models;
using Microsoft.Azure.Cosmos;

public static class DependencyInitializer
{
    public static IServiceCollection AddDIServices(this IServiceCollection services)
    {
        
        services.AddSingleton<CosmosClient>(serviceProvider =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var cosmosEndpoint = configuration["CosmosDb:Endpoint"];
            var cosmosKey = configuration["CosmosDb:Key"];
            return new CosmosClient(cosmosEndpoint, cosmosKey);
        });

        services.AddScoped<ICosmosService, CosmosService>();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Qurlify", Version = "v1" });
        });

        return services;
    }

    public static IApplicationBuilder UseOpenApi(this IApplicationBuilder builder)
    {
        builder.UseSwagger();
        builder.UseSwaggerUI(c =>
        {
            c.RoutePrefix = ""; c.SwaggerEndpoint("/swagger/v1/swagger.json", "Qurlify v0.1");
        });
        return builder;
    }
}