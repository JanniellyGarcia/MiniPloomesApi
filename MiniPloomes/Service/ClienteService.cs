using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Domain.Models;
using MiniPloomes.Infraestructure.DatabaseConnection.cs;
using MiniPloomes.Service.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace MiniPloomes.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IUsuarioService _usuarioService;

        public ClienteService(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<List<ClienteBuscadoResponse>> BuscaTodosOsClientesAsync()
        {
            DataBaseConnection connection = new DataBaseConnection();

            var Clientes = new List<ClienteBuscadoResponse>();

            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("SELECT * FROM cliente", connection.SqlConnection);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlDataReader = await connection.SqlCommand.ExecuteReaderAsync();

            while (connection.SqlDataReader.Read())
            {
                var cliente = new ClienteBuscadoResponse()
                {
                    Id = connection.SqlDataReader.GetInt32("IdCliente"),
                    IdUsuario = connection.SqlDataReader.GetInt32("IdUsuario"),
                    Nome = connection.SqlDataReader.GetString("NomeCliente"),
                    DataDeCriacao = connection.SqlDataReader.GetDateTime("DataDeCriacao"),
                    Usuario = await _usuarioService.BuscarUsuarioPorIdAsync(connection.SqlDataReader.GetInt32("IdUsuario")),
                };
                Clientes.Add(cliente);
            }

            connection.CloseConnection();

            return Clientes;
        }


        public async Task<ClienteBuscadoResponse> BuscarClientePorIdAsync(int idCliente)
        {
            DataBaseConnection connection = new DataBaseConnection();

            var clienteBuscado = new ClienteBuscadoResponse();

            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("SELECT * FROM cliente where IdCliente = @id", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@id", idCliente);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlDataReader = await connection.SqlCommand.ExecuteReaderAsync();

            if (!connection.SqlDataReader.HasRows)
                throw new($" Não Existe Nenhum Cliente Associado ao Id: {idCliente}");

            while (connection.SqlDataReader.Read())
            {

                clienteBuscado.Id = connection.SqlDataReader.GetInt32("IdCliente");
                clienteBuscado.IdUsuario = connection.SqlDataReader.GetInt32("IdUsuario");
                clienteBuscado.Nome = connection.SqlDataReader.GetString("NomeCliente");
                clienteBuscado.DataDeCriacao = connection.SqlDataReader.GetDateTime("DataDeCriacao");
                clienteBuscado.Usuario = await _usuarioService.BuscarUsuarioPorIdAsync(connection.SqlDataReader.GetInt32("IdUsuario"));

            }

            connection.CloseConnection();

            return clienteBuscado;
        }


        public async Task CriarClienteAsync(ClienteRequest NovoCliente)
        {

            DataBaseConnection connection = new DataBaseConnection();

            await _usuarioService.BuscarUsuarioPorIdAsync(NovoCliente.IdUsuario);

            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("INSERT INTO cliente (IdUsuario, NomeCliente, DataDeCriacao) VALUES (@idUsuario,@nome,@data)", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@idUsuario", NovoCliente.IdUsuario);
            connection.SqlCommand.Parameters.AddWithValue("@nome", NovoCliente.Nome);
            connection.SqlCommand.Parameters.AddWithValue("@data", DateTime.Now);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlCommand.ExecuteNonQuery();

            connection.CloseConnection();
        }


    }
}
