using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Domain.Models;

namespace MiniPloomes.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<List<UsuariobuscadoResponse>> BuscarTodosUsuariosAsync();
        Task<UsuarioResponse> BuscarUsuarioPorIdAsync(int IdUsuario);
        Task CriarUsuarioAsync(UsuarioRequest usuario);
    }
}
