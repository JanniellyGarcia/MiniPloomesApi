using MiniPloomes.Domain.DataTrasnferObject;

namespace MiniPloomes.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuariobuscadoResponse>> BuscarTodosUsuariosAsync();
        Task<UsuarioResponse> BuscarUsuarioPorIdAsync(int IdUsuario);
        Task CriarUsuarioAsync(UsuarioRequest usuario);
        Task AtualizarUsuarioAsync(UsuarioRequest usuario, int idUsuario);
        Task DeletarUsuarioAsync(int IdUsuario);
    }
}
