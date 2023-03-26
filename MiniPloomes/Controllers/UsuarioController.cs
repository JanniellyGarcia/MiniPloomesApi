using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Service.Interfaces;

namespace MiniPloomes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioClienteService _usuarioClienteService;

        public UsuarioController(IUsuarioService usuarioService, IUsuarioClienteService usuarioClienteService)
        {
            _usuarioService = usuarioService;
            _usuarioClienteService = usuarioClienteService;
        }


        /// <summary>
        ///    Retorna todos os usuários.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<UsuariobuscadoResponse>>> BuscarTodosUsuariosAsync()
        {
            var usuariosBuscados = await _usuarioService.BuscarTodosUsuariosAsync();
            return usuariosBuscados;
        }


        /// <summary>
        ///    Retorna o usuário pelo seu Id.
        /// </summary>
        [HttpGet("{idUsuario}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<UsuariobuscadoResponse>> BuscarUsuarioPorIdAsync(int idUsuario)
        {
            try
            {
                var usuarioBuscado = await _usuarioClienteService.BuscarUsuarioPorIdAsync(idUsuario);
                return usuarioBuscado;
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }


        /// <summary>
        ///   Armazena um novo usuário no banco de dados.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CriarUsuarioAsync([FromBody] UsuarioRequest novoUsuario)
        {
            await _usuarioService.CriarUsuarioAsync(novoUsuario);
            return NoContent();
        }


        /// <summary>
        ///   Atualiza as informações de um usuário a partir de um objeto de requisição e de seu Id.
        /// </summary>
        [HttpPut("{usuarioId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AtualizarUsuarioAsync([FromBody] UsuarioRequest novoUsuario, int usuarioId)
        {
            try
            {
                await _usuarioService.AtualizarUsuarioAsync(novoUsuario, usuarioId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }


        /// <summary>
        ///  Deleta usuário pelo seu Id.
        /// </summary>
        /// <remarks>
        ///     Observações:
        ///  
        ///     (1) - O usuário só será deletado caso não haja nenhum cliente associado.
        ///     
        /// 
        /// </remarks>
        [HttpDelete("{idUsuario}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeletaUsuarioAsync(int idUsuario)
        {
            try
            {
                await _usuarioService.DeletarUsuarioAsync(idUsuario);
                return NoContent();
            }
            catch (Exception ex)
            {

                return Conflict(ex.Message);
            }

        }
    }
}
