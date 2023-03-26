using Microsoft.AspNetCore.Mvc;
using MiniPloomes.Domain.DataTrasnferObject;
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


        /// <summary>
        ///     Retorna uma lista de todos os clientes armazenados.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<ClienteBuscadoResponse>>> BuscaTodosOsClientesAsync()
        {
            var clientesBuscados = await _clienteService.BuscaTodosOsClientesAsync();
            return clientesBuscados;
        }


        /// <summary>
        ///     Retorna um cliente a partir de seu Id.
        /// </summary>
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

        /// <summary>
        ///    Retorna uma lista de clientes a partir do usuário a qual eles estiverem associados.
        /// </summary>
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


        /// <summary>
        ///    Armazena um novo cliente no banco de dados.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> CriarClienteAsync([FromBody] ClienteRequest novoCliente)
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


        /// <summary>
        ///    Atualiza dados do cliente a partir de um objeto de requisição e do Id do cliente.
        /// </summary>
        /// <remarks>
        ///     Observações:
        ///  
        ///     (1) - Ao Atualizar o cliente, além de alterar na tabela de usuário também é alterado na tabela que possui a relação usuário-cliente.
        ///     
        ///     (2) - Ao Alterar o usuário a qual o cliente está associado, ele também é alterado na abela que possui a relação usuário-cliente.
        /// 
        /// </remarks>
        [HttpPut("{clienteId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> AtualizarClienteAsync([FromBody] ClienteRequest cliente, int clienteId)
        {
            try
            {
                await _clienteService.AtualizarClienteAsync(cliente, clienteId);
                return NoContent();
            }
            catch (Exception ex)
            {

                return NotFound(ex.Message);
            }

        }


        /// <summary>
        ///    Deleta cliente a partir de seu Id.
        /// </summary>
        /// <remarks>
        ///     Observações:
        ///  
        ///     (1) - Ao deletar o cliente, além de alterar na tabela de usuário também é deletado na tabela que possui a relação usuário-cliente.
        ///     
        /// 
        /// </remarks>
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
