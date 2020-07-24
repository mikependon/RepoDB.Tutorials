using RepoDbDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoDbDemo.Interfaces
{
    public interface ISignalRepository
    {
        Task<int> SaveAllAsync(IEnumerable<Signal> signals);
        Task<int> SaveAsync(Signal signal);
        Task<Signal> GetAsync(int id);
        Task<IEnumerable<Signal>> GetAllAsync();
    }
}
