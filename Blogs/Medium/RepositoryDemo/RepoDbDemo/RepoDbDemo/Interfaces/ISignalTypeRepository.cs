using RepoDbDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoDbDemo.Interfaces
{
    public interface ISignalTypeRepository
    {
        Task<IEnumerable<SignalType>> GetAllAsync();
    }
}
