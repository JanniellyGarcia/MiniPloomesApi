using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Domain.Models;

namespace MiniPloomes.Service.Interfaces
{
    public interface IUsuarioClienteService
    {
        Task<List<ClienteResponse>> BuscaClientePorUsuarioAsync(int UsuarioId);
        Task<UsuariobuscadoResponse> BuscarUsuarioPorIdAsync(int UsuarioId);
        Task<bool> VerificaSeUsuarioPossuiClienteAsync(int UsuarioId);
        Task  DeletarRelacaoAsync(int idCliente);
    }
}
