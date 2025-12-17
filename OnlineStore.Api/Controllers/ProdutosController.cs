using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Models;
using OnlineStore.Api.Services;

namespace OnlineStore.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProdutosController : ControllerBase
{
    private readonly IProdutoService _produtoService;

    public ProdutosController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Produto>>> GetTodos()
    {
        var produtos = await _produtoService.ListarTodosAsync();
        return Ok(produtos);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Produto>> GetPorId(int id)
    {
        var produto = await _produtoService.BuscarPorIdAsync(id);
        if (produto == null)
            return NotFound();

        return Ok(produto);
    }

    [HttpGet("pesquisar")]
    public async Task<ActionResult<IEnumerable<Produto>>> BuscarPorNome([FromQuery] string nome)
    {
        var produtos = await _produtoService.BuscarPorNomeAsync(nome);
        return Ok(produtos);
    }

    [HttpGet("contar")]
    public async Task<ActionResult<int>> Contar()
    {
        var total = await _produtoService.ContarAsync();
        return Ok(total);
    }

    [HttpPost]
    public async Task<ActionResult<Produto>> Criar([FromBody] Produto produto)
    {
        var criado = await _produtoService.CriarAsync(produto);
        return CreatedAtAction(nameof(GetPorId), new { id = criado.Id }, criado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] Produto produto)
    {
        var atualizado = await _produtoService.AtualizarAsync(id, produto);
        if (!atualizado)
            return NotFound();

        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Deletar(int id)
    {
        var deletado = await _produtoService.DeletarAsync(id);
        if (!deletado)
            return NotFound();

        return NoContent();
    }
}
