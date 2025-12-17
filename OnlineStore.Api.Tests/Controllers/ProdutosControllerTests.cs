using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStore.Api.Controllers;
using OnlineStore.Api.Models;
using OnlineStore.Api.Services;
using Xunit;

namespace OnlineStore.Api.Tests.Controllers;

public class ProdutosControllerTests
{
    [Fact]
    public async Task GetTodos_DeveRetornarOkComLista()
    {
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.ListarTodosAsync())
            .ReturnsAsync(new List<Produto> { new() { Id = 1, Nome = "P1" } });

        var controller = new ProdutosController(mockService.Object);

        var result = await controller.GetTodos();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var lista = Assert.IsAssignableFrom<IEnumerable<Produto>>(okResult.Value);
        Assert.Single(lista);
    }

    [Fact]
    public async Task GetPorId_QuandoNaoEncontrado_DeveRetornarNotFound()
    {
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.BuscarPorIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Produto?)null);

        var controller = new ProdutosController(mockService.Object);

        var result = await controller.GetPorId(1);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task Criar_DeveRetornarCreatedAtAction()
    {
        var mockService = new Mock<IProdutoService>();
        var produto = new Produto { Id = 1, Nome = "Novo" };
        mockService.Setup(s => s.CriarAsync(It.IsAny<Produto>()))
            .ReturnsAsync(produto);

        var controller = new ProdutosController(mockService.Object);

        var result = await controller.Criar(new Produto { Nome = "Novo" });

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.Equal(nameof(ProdutosController.GetPorId), created.ActionName);
        Assert.Equal(produto, created.Value);
    }

    [Fact]
    public async Task Deletar_QuandoNaoEncontrado_DeveRetornarNotFound()
    {
        var mockService = new Mock<IProdutoService>();
        mockService.Setup(s => s.DeletarAsync(It.IsAny<int>()))
            .ReturnsAsync(false);

        var controller = new ProdutosController(mockService.Object);

        var result = await controller.Deletar(1);

        Assert.IsType<NotFoundResult>(result);
    }
}
