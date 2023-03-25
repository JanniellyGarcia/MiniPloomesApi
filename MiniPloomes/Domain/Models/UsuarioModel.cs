namespace MiniPloomes.Domain.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }

        public virtual List<ClienteModel>? Clientes { get; set; }

    }
}
