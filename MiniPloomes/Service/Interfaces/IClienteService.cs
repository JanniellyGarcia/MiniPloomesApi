using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Domain.Models;

namespace MiniPloomes.Service.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteBuscadoResponse>> BuscaTodosOsClientesAsync();
        Task<ClienteBuscadoResponse> BuscarClientePorIdAsync(int idCliente);
        Task CriarClienteAsync(ClienteRequest NovoCliente);
        Task DeletarClienteAsync(int idCliente);
    }
}
