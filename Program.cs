using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TisCircuitsAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Authentification Windows
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
    .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

// CORS pour autoriser Angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // ⚠️ doit matcher Angular
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // Pour cookies Windows Auth
    });
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// BDD
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// JSON - Ignore cycles
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TisCircuitsAPI v1");
    c.RoutePrefix = "swagger";
});

// ⛔ DOIT être ici, AVANT Authentication
app.UseCors("AllowAngularDev");

// Sécurité
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
// Contrôleurs
app.MapControllers();

// Lancer l'app
app.Run();
