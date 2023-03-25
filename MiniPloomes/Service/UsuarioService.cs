using MiniPloomes.Domain.DataTrasnferObject;
using MiniPloomes.Domain.Models;
using MiniPloomes.Infraestructure.DatabaseConnection.cs;
using MiniPloomes.Service.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace MiniPloomes.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioClienteService _usuarioClienteService;

        public UsuarioService(IUsuarioClienteService usuarioClienteService)
        {
            _usuarioClienteService = usuarioClienteService;

        }

       

        public async Task<List<UsuariobuscadoResponse>> BuscarTodosUsuariosAsync()
        {
            DataBaseConnection connection = new DataBaseConnection();

            var usuarios = new List<UsuariobuscadoResponse>();

            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("SELECT * FROM usuario", connection.SqlConnection);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlDataReader = await connection.SqlCommand.ExecuteReaderAsync();
           
            while (connection.SqlDataReader.Read())
            {
                var verificaStatusUsuario = await _usuarioClienteService.VerificaSeUsuarioPossuiClienteAsync(connection.SqlDataReader.GetInt32("IdUsuario"));
                var usuario = new UsuariobuscadoResponse()
                {
                    Id = connection.SqlDataReader.GetInt32("IdUsuario"),
                    Nome = connection.SqlDataReader.GetString("NomeUsuario"),
                    Email = connection.SqlDataReader.GetString("Email"),
                    Clientes = verificaStatusUsuario  ? await _usuarioClienteService.BuscaClientePorUsuarioAsync(connection.SqlDataReader.GetInt32("IdUsuario")) : null,
            };
                usuarios.Add(usuario);
            }

            connection.CloseConnection();

            return usuarios;    
        }

        public async Task<UsuarioResponse> BuscarUsuarioPorIdAsync(int IdUsuario)
        {
            var usuarioEncontrado = new UsuarioResponse();
            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("SELECT * FROM usuario WHERE IdUsuario = @id", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@id", IdUsuario);
            connection.SqlCommand.CommandType = CommandType.Text;
            connection.SqlDataReader = await connection.SqlCommand.ExecuteReaderAsync();

            if (!connection.SqlDataReader.HasRows)
                throw new Exception($" Não Existe Nenhum Usuário Associado ao Id: {IdUsuario}");

            while (connection.SqlDataReader.Read())
            {

                usuarioEncontrado.Id = connection.SqlDataReader.GetInt32("IdUsuario");
                usuarioEncontrado.Nome = connection.SqlDataReader.GetString("NomeUsuario");
                usuarioEncontrado.Email = connection.SqlDataReader.GetString("Email");

            }
            connection.CloseConnection();
            return usuarioEncontrado;
        }

        public async Task CriarUsuarioAsync(UsuarioRequest usuario)
        {

            DataBaseConnection connection = new DataBaseConnection();

            connection.GetConnection();

            connection.SqlCommand = new SqlCommand("INSERT INTO usuario (NomeUsuario, Email) VALUES (@nome, @email)", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@nome", usuario.Nome);
            connection.SqlCommand.Parameters.AddWithValue("@email", usuario.Email);
            connection.SqlCommand.CommandType = CommandType.Text;
            await connection.SqlCommand.ExecuteNonQueryAsync();
            
            connection.CloseConnection();

        }

        public async Task AtualizarUsuarioAsync(UsuarioRequest usuario,  int idUsuario)
        {
            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();
            await BuscarUsuarioPorIdAsync(idUsuario);

            var vericaStatusUsuario = await _usuarioClienteService.VerificaSeUsuarioPossuiClienteAsync(idUsuario);

            if (vericaStatusUsuario)
               await _usuarioClienteService.AtualizarUsuarioAsync(usuario, idUsuario);

            connection.SqlCommand = new SqlCommand("UPDATE usuario SET NomeUsuario = @nome, Email = @email WHERE IdUsuario = @id", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@id", idUsuario);
            connection.SqlCommand.Parameters.AddWithValue("@nome", usuario.Nome);
            connection.SqlCommand.Parameters.AddWithValue("@email", usuario.Email);
            connection.SqlCommand.CommandType = CommandType.Text;
            await connection.SqlCommand.ExecuteNonQueryAsync();

            connection.CloseConnection();
        }

        public async Task DeletarUsuarioAsync(int IdUsuario)
        {
            DataBaseConnection connection = new DataBaseConnection();
            connection.GetConnection();
            await BuscarUsuarioPorIdAsync(IdUsuario);

            var vericaStatusUsuario = await _usuarioClienteService.VerificaSeUsuarioPossuiClienteAsync(IdUsuario);

            if (vericaStatusUsuario)
                throw new Exception("Usuário Não Deletado. Existem Clientes Atrelados a Este Usuário.");

            connection.SqlCommand = new SqlCommand("DELETE FROM usuario WHERE IdUsuario = @idUsuario", connection.SqlConnection);
            connection.SqlCommand.Parameters.AddWithValue("@idUsuario", IdUsuario);
            connection.SqlCommand.CommandType = CommandType.Text;
            await connection.SqlCommand.ExecuteNonQueryAsync();

            connection.CloseConnection();

        }
    }
}
