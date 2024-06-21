var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDIServices();

var app = builder.Build();

var endpoint = app.Services.GetService<IEndpoint>();
endpoint?.RegisterRoutes(app);

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.UseHttpsRedirection();


app.Run();
