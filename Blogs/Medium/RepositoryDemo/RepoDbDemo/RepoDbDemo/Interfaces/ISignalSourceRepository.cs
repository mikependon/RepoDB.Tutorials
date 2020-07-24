using RepoDbDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RepoDbDemo.Interfaces
{
    public interface ISignalSourceRepository
    {
        Task<IEnumerable<SignalSource>> GetAllAsync();
    }
}
