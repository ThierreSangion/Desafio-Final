using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Data;
using OnlineStore.Api.Models;
using OnlineStore.Api.Services;
using Xunit;

namespace OnlineStore.Api.Tests.Services;

public class ProdutoServiceTests
{
    private static AppDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CriarAsync_DevePersistirProduto()
    {
        using var context = CreateInMemoryContext();
        var service = new ProdutoService(context);
        var produto = new Produto { Nome = "Produto A", Descricao = "Teste", Preco = 10m, Estoque = 5 };

        var criado = await service.CriarAsync(produto);

        Assert.True(criado.Id > 0 || context.Produtos.Any());
        var noBanco = await context.Produtos.FirstOrDefaultAsync();
        Assert.NotNull(noBanco);
        Assert.Equal("Produto A", noBanco!.Nome);
    }

    [Fact]
    public async Task BuscarPorIdAsync_DeveRetornarProdutoQuandoExiste()
    {
        using var context = CreateInMemoryContext();
        var produto = new Produto { Nome = "Produto B", Descricao = "Teste", Preco = 20m, Estoque = 3 };
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var service = new ProdutoService(context);
        var resultado = await service.BuscarPorIdAsync(produto.Id);

        Assert.NotNull(resultado);
        Assert.Equal(produto.Nome, resultado!.Nome);
    }

    [Fact]
    public async Task AtualizarAsync_DeveRetornarFalseQuandoNaoEncontrado()
    {
        using var context = CreateInMemoryContext();
        var service = new ProdutoService(context);
        var produto = new Produto { Nome = "Produto X", Descricao = "Teste", Preco = 10m, Estoque = 1 };

        var resultado = await service.AtualizarAsync(999, produto);

        Assert.False(resultado);
    }

    [Fact]
    public async Task DeletarAsync_DeveRemoverProduto()
    {
        using var context = CreateInMemoryContext();
        var produto = new Produto { Nome = "Produto C", Descricao = "Teste", Preco = 30m, Estoque = 10 };
        context.Produtos.Add(produto);
        await context.SaveChangesAsync();

        var service = new ProdutoService(context);

        var sucesso = await service.DeletarAsync(produto.Id);

        Assert.True(sucesso);
        Assert.False(context.Produtos.Any());
    }
}
