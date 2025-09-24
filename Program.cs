using API_spotify_att.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Serviços
// ======================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS – permitir qualquer origem (para frontend local)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

// Conexão com SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao"))
);

var app = builder.Build();

// ======================
// Criar pastas automaticamente
// ======================
var webRoot = app.Environment.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(webRoot))
    Directory.CreateDirectory(webRoot);

var uploadsPath = Path.Combine(webRoot, "Uploads");
if (!Directory.Exists(uploadsPath))
    Directory.CreateDirectory(uploadsPath);

// ======================
// Middleware
// ======================

// CORS deve vir antes de MapControllers
app.UseCors("AllowAll");

// Swagger (apenas em Development)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// NÃO usar HTTPS redirect para evitar problemas com HTTP local
// app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthorization();

// Mapear controllers
app.MapControllers();

app.Run();
