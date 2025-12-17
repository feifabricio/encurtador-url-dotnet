using Microsoft.EntityFrameworkCore;
using EncurtadorUrl.Aplicacao.Interfaces;
using EncurtadorUrl.Infraestrutura.Dados;
using EncurtadorUrl.Infraestrutura.Servicos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextoAplicacao>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IServicoEncurtadorUrl, ServicoEncurtadorUrl>();

var app = builder.Build();

app.MapControllers();

app.Run();