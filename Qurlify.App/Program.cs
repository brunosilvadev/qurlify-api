var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDIServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
}

app.UseHttpsRedirection();
app.UseEndpoints();
app.ConfigureCors();
app.Run();
