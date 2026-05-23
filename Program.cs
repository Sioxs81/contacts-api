using ContactsApi.Data;
using ContactsApi.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=contactos.db"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Crear BD y datos iniciales al arrancar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Contacts.Any())
    {
        db.Contacts.AddRange(
            new() { Nombre = "Juan Pérez",        Telefono = "8888-1111" },
            new() { Nombre = "María González",    Telefono = "7777-2222" },
            new() { Nombre = "Carlos Rodríguez",  Telefono = "6666-3333" },
            new() { Nombre = "Ana Jiménez",       Telefono = "5555-4444" },
            new() { Nombre = "Luis Mora",         Telefono = "8765-4321" },
            new() { Nombre = "Sofía Vargas",      Telefono = "7654-3210" }
        );
        db.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAngular");
app.UseMiddleware<ApiKeyMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
