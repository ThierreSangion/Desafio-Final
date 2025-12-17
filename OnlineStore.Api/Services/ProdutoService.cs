using Microsoft.EntityFrameworkCore;
using OnlineStore.Api.Data;
using OnlineStore.Api.Models;

namespace OnlineStore.Api.Services;

public class ProdutoService : IProdutoService
{
    private readonly AppDbContext _context;

    public ProdutoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> ListarTodosAsync()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<Produto?> BuscarPorIdAsync(int id)
    {
        return await _context.Produtos.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Produto>> BuscarPorNomeAsync(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            return Enumerable.Empty<Produto>();

        return await _context.Produtos
            .AsNoTracking()
            .Where(p => p.Nome.Contains(nome))
            .ToListAsync();
    }

    public async Task<int> ContarAsync()
    {
        return await _context.Produtos.CountAsync();
    }

    public async Task<Produto> CriarAsync(Produto produto)
    {
        _context.Produtos.Add(produto);
        await _context.SaveChangesAsync();
        return produto;
    }

    public async Task<bool> AtualizarAsync(int id, Produto produto)
    {
        var existente = await _context.Produtos.FindAsync(id);
        if (existente == null)
            return false;

        existente.Nome = produto.Nome;
        existente.Descricao = produto.Descricao;
        existente.Preco = produto.Preco;
        existente.Estoque = produto.Estoque;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var existente = await _context.Produtos.FindAsync(id);
        if (existente == null)
            return false;

        _context.Produtos.Remove(existente);
        await _context.SaveChangesAsync();
        return true;
    }
}
