using Microsoft.EntityFrameworkCore;
using diplo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Aplicar migraciones automáticamente
using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
  dbContext.Database.Migrate(); // ← Esto crea/actualiza la base de datos
    //   int retries = 10;
    // for (int i = 0; i < retries; i++)
    // {
    //     try
    //     {
    //         dbContext.Database.Migrate();
    //         break;
    //     }
    //     catch (Exception ex) when (i < retries - 1)
    //     {
    //         Console.WriteLine($"Error al migrar (intento {i+1}/{retries}): {ex.Message}");
    //         await Task.Delay(5000);
    //     }
    // }
}


app.UseSwagger();
app.UseSwaggerUI();

// UseHttpsRedirection solo en desarrollo local, no en Azure (usa proxy inverso)
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.Run();