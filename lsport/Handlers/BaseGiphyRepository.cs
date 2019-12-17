using lsport.Interfaces;
using lsport.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lsport.Handlers
{
    public abstract class BaseGiphyRepository : IRepository<Giphy>
    {
        public ActionResultEnum Update(int id, Giphy item)
        {
            item.UpdateTime = DateTime.Now;
            return UpdateInternal(id, item);
        }
        public abstract ActionResultEnum UpdateInternal(int id, Giphy item);
        public abstract Task<bool> Create(Giphy obj);
        public abstract Giphy Retrieve(int id);
        public abstract List<Giphy> RetrieveAll();
        public abstract Giphy Delete(int id);
    }
}