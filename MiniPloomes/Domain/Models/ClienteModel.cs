namespace MiniPloomes.Domain.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }  
        public string? Nome { get; set; }
        public DateTime? DataDeCriacao { get; set; }
        public virtual UsuarioModel? Usuario { get; set; }
    }
}
