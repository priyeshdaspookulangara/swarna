using GoldLoan.Application.DTOs;
using GoldLoan.Application.Interfaces;
using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace GoldLoan.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientDto?> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                return null;
            }

            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                ContactNumber = client.ContactNumber,
                IdProofDetails = client.IdProofDetails
            };
        }

        public async Task<IReadOnlyList<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.ListAllAsync();
            return clients.Select(client => new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                ContactNumber = client.ContactNumber,
                IdProofDetails = client.IdProofDetails
            }).ToList().AsReadOnly();
        }

        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            var client = new Client
            {
                Name = clientDto.Name,
                Address = clientDto.Address,
                ContactNumber = clientDto.ContactNumber,
                IdProofDetails = clientDto.IdProofDetails
            };

            var newClient = await _clientRepository.AddAsync(client);
            clientDto.Id = newClient.Id;
            return clientDto;
        }

        public async Task UpdateClientAsync(int id, ClientDto clientDto)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                // Or throw a custom NotFoundException
                return;
            }

            client.Name = clientDto.Name;
            client.Address = clientDto.Address;
            client.ContactNumber = clientDto.ContactNumber;
            client.IdProofDetails = clientDto.IdProofDetails;

            await _clientRepository.UpdateAsync(client);
        }

        public async Task DeleteClientAsync(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                // Or throw a custom NotFoundException
                return;
            }

            await _clientRepository.DeleteAsync(client);
        }
    }
}
