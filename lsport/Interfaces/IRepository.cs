using System.Collections.Generic;
using System.Threading.Tasks;
using lsport.Models;

namespace lsport.Interfaces
{
    public interface IRepository<in T>
    {
        Task<bool> Create(T obj);
        Giphy Retrieve(int id);
        List<Giphy> RetrieveAll();
        ActionResultEnum Update(int id, T key);
        Giphy Delete(int id);
    }
}
