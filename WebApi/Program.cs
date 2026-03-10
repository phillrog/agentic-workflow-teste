using Domain.Interfaces;
using Domain.Validators;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Injeção do DbContext (SQL Server por padrão, mas configurável)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    if (builder.Environment.IsEnvironment("Test"))
        options.UseInMemoryDatabase("TestDb");
    else
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Injeção de Dependência
builder.Services.AddValidatorsFromAssemblyContaining<UsuarioValidator>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Swagger Documentation

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();