using Microsoft.EntityFrameworkCore;
using PlanosSaude.API.Data;
using PlanosSaude.API.Middlewares;
using PlanosSaude.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PlanosSaudeDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PlanosSaudeConnection")));

// DI
builder.Services.AddScoped<IBeneficiarioService, BeneficiarioService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.Run();

