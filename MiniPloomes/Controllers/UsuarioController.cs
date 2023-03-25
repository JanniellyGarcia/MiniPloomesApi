using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Domain.Models;
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

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<UsuariobuscadoResponse>>> BuscarTodosUsuariosAsync()
        {
            var usuariosBuscados = await _usuarioService.BuscarTodosUsuariosAsync();
            return usuariosBuscados;
        }

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

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CriarUsuarioAsync([FromBody]UsuarioRequest novoUsuario)
        {
            await _usuarioService.CriarUsuarioAsync(novoUsuario);
            return NoContent();
        }

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
