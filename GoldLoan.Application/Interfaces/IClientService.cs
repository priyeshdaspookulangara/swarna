using GoldLoan.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientDto?> GetClientByIdAsync(int id);
        Task<IReadOnlyList<ClientDto>> GetAllClientsAsync();
        Task<ClientDto> CreateClientAsync(ClientDto clientDto);
        Task UpdateClientAsync(int id, ClientDto clientDto);
        Task DeleteClientAsync(int id);
    }
}
