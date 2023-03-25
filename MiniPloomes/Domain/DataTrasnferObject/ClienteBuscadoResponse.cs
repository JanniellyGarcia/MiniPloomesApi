namespace MiniPloomes.Domain.DataTrasnferObject
{
    public class ClienteBuscadoResponse
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string? Nome { get; set; }
        public DateTime? DataDeCriacao { get; set; }
        public  UsuarioResponse? Usuario { get; set; }
    }
}
