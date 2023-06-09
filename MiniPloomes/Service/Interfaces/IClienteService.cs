﻿using MiniPloomes.Domain.DataTrasnferObject;

namespace MiniPloomes.Service.Interfaces
{
    public interface IClienteService
    {
        Task<List<ClienteBuscadoResponse>> BuscaTodosOsClientesAsync();
        Task<ClienteBuscadoResponse> BuscarClientePorIdAsync(int idCliente);
        Task CriarClienteAsync(ClienteRequest NovoCliente);
        Task AtualizarClienteAsync(ClienteRequest NovoCliente, int idCliente);
        Task DeletarClienteAsync(int idCliente);
    }
}
