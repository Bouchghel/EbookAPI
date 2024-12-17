using Microsoft.EntityFrameworkCore;
using PLivres.Data;
using Microsoft.OpenApi.Models;
using PLivres.Service;

var builder = WebApplication.CreateBuilder(args);

// Ajouter le contexte de base de données
builder.Services.AddDbContext<BookContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enregistrer IBookMappingService,IBookService et FileService
builder.Services.AddScoped<IBookMappingService, BookMappingService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<FileService>();


// Ajouter les services pour les contrôleurs
builder.Services.AddControllers();

// Configurer Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.MapType<IFormFile>(() => new OpenApiSchema { Type = "string", Format = "binary" });

    // Autres configurations Swagger
});

// Ajouter CORS 
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

// Servir les fichiers statiques (pour les fichiers uploadés)
app.UseStaticFiles();

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


app.UseAuthorization();

app.MapControllers();

app.Run();
