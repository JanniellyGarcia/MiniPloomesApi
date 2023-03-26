using MiniPloomes.Domain.DataTrasnferObject;

namespace MiniPloomes.Service.Interfaces
{
    public interface IUsuarioClienteService
    {
        Task<List<ClienteResponse>> BuscaClientePorUsuarioAsync(int usuarioId);
        Task<UsuariobuscadoResponse> BuscarUsuarioPorIdAsync(int usuarioId);
        Task<bool> VerificaSeUsuarioPossuiClienteAsync(int usuarioId);
        Task AtualizarUsuarioAsync(UsuarioRequest usuario, int usuarioId);
        Task AtualizarClienteAsync(ClienteRequest cliente, int clienteId);
        Task DeletarRelacaoAsync(int idCliente);
    }
}
