using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Infraestructure.DatabaseConnection.cs;
using MiniPloomes.Service.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace MiniPloomes.Service
{

    public class UsuarioClienteService : IUsuarioClienteService
    {


        public async Task<List<ClienteResponse>> BuscaClientePorUsuarioAsync(int UsuarioId)
        {
            DataBaseConnection connection = new DataBaseConnection();

            var ClientesBuscados = new List<ClienteResponse>();

            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("SELECT * FROM usuario_cliente WHERE IdUsuario = @idUsuario", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@idUsuario", UsuarioId);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlDataReader = await connection.SqlCommand.ExecuteReaderAsync();

            if (!connection.SqlDataReader.HasRows)
                throw new($" Não Existe Nenhum Cliente Associado ao Id: {UsuarioId}");

            while (connection.SqlDataReader.Read())
            {
                var cliente = new ClienteResponse()
                {
                    Id = connection.SqlDataReader.GetInt32("IdCliente"),
                    Nome = connection.SqlDataReader.GetString("NomeCliente"),
                    DataDeCriacao = connection.SqlDataReader.GetDateTime("DataCriacaoCliente"),
                };
                ClientesBuscados.Add(cliente);
            }

            connection.CloseConnection();

            return ClientesBuscados;
        }

        public async Task<UsuariobuscadoResponse> BuscarUsuarioPorIdAsync(int UsuarioId)
        {
            var usuarioEncontrado = new UsuariobuscadoResponse();
            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("SELECT * FROM usuario WHERE IdUsuario = @id", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@id", UsuarioId);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlDataReader = await connection.SqlCommand.ExecuteReaderAsync();

            if (!connection.SqlDataReader.HasRows)
                throw new Exception($" Não Existe Nenhum Usuário Associado ao Id: {UsuarioId}");

            while (connection.SqlDataReader.Read())
            {
                var verificaStatusUsuario = await VerificaSeUsuarioPossuiClienteAsync(UsuarioId);

                usuarioEncontrado.Id = connection.SqlDataReader.GetInt32("IdUsuario");
                usuarioEncontrado.Nome = connection.SqlDataReader.GetString("NomeUsuario");
                usuarioEncontrado.Email = connection.SqlDataReader.GetString("Email");
                usuarioEncontrado.Clientes = verificaStatusUsuario ? await BuscaClientePorUsuarioAsync(UsuarioId) : null;

            }
            connection.CloseConnection();
            return usuarioEncontrado;
        }

        public async Task DeletarRelacaoAsync(int idCliente)
        {
            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();


            connection.SqlCommand = new SqlCommand("DELETE FROM usuario_cliente WHERE IdCliente = @idCliente", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@idCliente", idCliente);
            connection.SqlCommand.CommandType = CommandType.Text;
            await connection.SqlCommand.ExecuteNonQueryAsync();

            connection.CloseConnection();
        }

        public async Task AtualizarUsuarioAsync(UsuarioRequest usuario, int usuarioId)
        {
            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("UPDATE usuario_cliente SET NomeUsuario = @nome, EmailUsuario = @email WHERE IdUsuario = @id", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@id", usuarioId);
            connection.SqlCommand.Parameters.AddWithValue("@nome", usuario.Nome);
            connection.SqlCommand.Parameters.AddWithValue("@email", usuario.Email);
            connection.SqlCommand.CommandType = CommandType.Text;
            await connection.SqlCommand.ExecuteNonQueryAsync();

            connection.CloseConnection();
        }


        public async Task AtualizarClienteAsync(ClienteRequest cliente, int clienteId)
        {

            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();

            var novoUsuarioParaCliente = await BuscarUsuarioPorIdAsync(cliente.IdUsuario);

            connection.SqlCommand = new SqlCommand("UPDATE usuario_cliente SET IdUsuario = @idUsuario, NomeUsuario = @nomeUsuario, EmailUsuario = @emailUsuario, NomeCliente = @nomeCliente WHERE IdCliente = @idCliente", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@idCliente", clienteId);
            connection.SqlCommand.Parameters.AddWithValue("@idUsuario", novoUsuarioParaCliente.Id);
            connection.SqlCommand.Parameters.AddWithValue("@nomeUsuario", novoUsuarioParaCliente.Nome);
            connection.SqlCommand.Parameters.AddWithValue("@emailUsuario", novoUsuarioParaCliente.Email);
            connection.SqlCommand.Parameters.AddWithValue("@nomeCliente", cliente.Nome);
            connection.SqlCommand.CommandType = CommandType.Text;
            await connection.SqlCommand.ExecuteNonQueryAsync();

            connection.CloseConnection();
        }


        public async Task<bool> VerificaSeUsuarioPossuiClienteAsync(int UsuarioId)
        {
            var usuarioEncontrado = new UsuarioResponse();
            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("SELECT * FROM usuario_cliente WHERE IdUsuario = @id", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@id", UsuarioId);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlDataReader = await connection.SqlCommand.ExecuteReaderAsync();

            if (!connection.SqlDataReader.HasRows)
                return false;

            while (connection.SqlDataReader.Read())
            {

                usuarioEncontrado.Id = connection.SqlDataReader.GetInt32("IdUsuario");
                usuarioEncontrado.Nome = connection.SqlDataReader.GetString("NomeUsuario");
                usuarioEncontrado.Email = connection.SqlDataReader.GetString("EmailUsuario");

            }
            connection.CloseConnection();

            return true;
        }


    }


}
