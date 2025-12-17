using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Data;
using OnlineStore.Api.Models;
using OnlineStore.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("OnlineStoreDb"));

builder.Services.AddScoped<IProdutoService, ProdutoService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    if (!context.Produtos.Any())
    {
        context.Produtos.AddRange(
            new Produto { Nome = "Notebook", Descricao = "Notebook básico", Preco = 3500m, Estoque = 10 },
            new Produto { Nome = "Mouse", Descricao = "Mouse sem fio", Preco = 120m, Estoque = 50 },
            new Produto { Nome = "Teclado", Descricao = "Teclado mecânico", Preco = 280m, Estoque = 20 }
        );

        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
