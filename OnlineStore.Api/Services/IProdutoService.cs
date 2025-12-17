using OnlineStore.Api.Models;

namespace OnlineStore.Api.Services;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> ListarTodosAsync();
    Task<Produto?> BuscarPorIdAsync(int id);
    Task<IEnumerable<Produto>> BuscarPorNomeAsync(string nome);
    Task<int> ContarAsync();
    Task<Produto> CriarAsync(Produto produto);
    Task<bool> AtualizarAsync(int id, Produto produto);
    Task<bool> DeletarAsync(int id);
}
