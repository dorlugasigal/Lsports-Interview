using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using lsport.Models;

namespace lsport.Interfaces
{
    public interface IManager
    {
        public Task<Giphy> Add(string name);
        public List<Giphy> GetAll();
        public Giphy Get(int id);
        public Giphy Delete(int id);
        public Task<ActionResultEnum> Update(int id, string giphy);
    }

}