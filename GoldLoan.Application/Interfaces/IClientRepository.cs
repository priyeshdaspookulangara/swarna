using GoldLoan.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoldLoan.Application.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetByIdAsync(int id);
        Task<IReadOnlyList<Client>> ListAllAsync();
        Task<Client> AddAsync(Client entity);
        Task UpdateAsync(Client entity);
        Task DeleteAsync(Client entity);
    }
}
