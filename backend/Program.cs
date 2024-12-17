using Microsoft.EntityFrameworkCore;
using PLivres.Data;
using AutoMapper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Ajouter le contexte de base de données
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajouter AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Enregistrer IBookMappingService
builder.Services.AddScoped<IBookMappingService, BookMappingService>();

// Ajouter les services pour les contrôleurs
builder.Services.AddControllers();

// Configurer Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });

    // Autres configurations Swagger
});

// Ajouter CORS (si nécessaire pour un frontend)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure le pipeline des requêtes HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Activer HTTPS redirection
app.UseHttpsRedirection();

// Activer CORS
app.UseCors("AllowAll");

// Servir les fichiers statiques (pour les fichiers uploadés)
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
