using Microsoft.EntityFrameworkCore;
using ECommerceAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Adicionar serviços ao container
// Configurar o DbContext
builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    // SQLLite 
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var scope = app.Services.CreateScope();
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ECommerceDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Erro ao popular o banco de dados.");
    }
}

// Configure o pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();