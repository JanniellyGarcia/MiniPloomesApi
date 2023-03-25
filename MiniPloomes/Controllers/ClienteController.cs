using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Domain.Models;
using MiniPloomes.Service.Interfaces;

namespace MiniPloomes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IUsuarioClienteService _usuarioClienteService;
        public ClienteController(IClienteService clienteService, IUsuarioClienteService usuarioClienteService)
        {
            _clienteService = clienteService;
            _usuarioClienteService = usuarioClienteService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ClienteBuscadoResponse>>> BuscaTodosOsClientesAsync()
        {
            var clientesBuscados = await _clienteService.BuscaTodosOsClientesAsync();
            return clientesBuscados;
        }

        [HttpGet("{idCliente}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<ClienteBuscadoResponse>> BuscarClientePorIdAsync(int idCliente)
        {
            try
            {
                var clienteBuscado = await _clienteService.BuscarClientePorIdAsync(idCliente);
                return clienteBuscado;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
           
        }

        [HttpGet("Usuario/{idUsuario}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ClienteResponse>>> BuscaClientePorUsuarioAsync(int idUsuario)
        {
            try
            {
                var clientesBuscados = await _usuarioClienteService.BuscaClientePorUsuarioAsync(idUsuario);
                return clientesBuscados;
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CriarClienteAsync([FromBody]ClienteRequest novoCliente)
        {
            try
            {
                await _clienteService.CriarClienteAsync(novoCliente);
                return Ok();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }
           
        }

        [HttpDelete("{idCliente}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeletarClienteAsync(int idCliente)
        {
            try
            {
                await _clienteService.DeletarClienteAsync(idCliente);
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }
    }
}
