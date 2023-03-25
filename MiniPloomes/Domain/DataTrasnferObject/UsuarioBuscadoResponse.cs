namespace MiniPloomes.Domain.DataTrasnferObject
{
    public class UsuariobuscadoResponse
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public virtual List<ClienteResponse> Clientes { get; set; }
    }
}
