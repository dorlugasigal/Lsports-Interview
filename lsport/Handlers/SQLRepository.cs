//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using lsport.Interfaces;
//using lsport.Models;
//using Microsoft.EntityFrameworkCore;


//this is an implementation for Entity framework, which needs work and i don't have enough time




//namespace lsport.Handlers
//{
//    public class SQLRepository : BaseGiphyRepository
//    {
//        private readonly lsportContext _context;

//        public SQLRepository(lsportContext context)
//        {
//            _context = context;
//        }

//        public override async Task<ActionResultEnum> UpdateInternal(int id, Giphy giphy)
//        {
//            if (id != giphy.ID)
//            {
//                return ActionResultEnum.BadRequest;
//            }

//            _context.Entry(giphy).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//                return ActionResultEnum.Success;
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!GiphyExists(id))
//                {
//                    return ActionResultEnum.NotFound;
//                }
//                else
//                {
//                    throw;
//                }
//            }
//        }

//        public override async Task<bool> Create(Giphy giphy)
//        {
//            _context.Giphy.Add(giphy);
//            var ret = await _context.SaveChangesAsync();
//            return ret > 0;
//        }

//        public override Giphy Retrieve(int id)
//        {
//            return _context.Giphy.FindAsync(id);
//        }

//        public override List<Giphy> RetrieveAll()
//        {
//            return _context.Giphy.ToListAsync();
//        }

//        public override async Task<Giphy> Delete(int id)
//        {
//            var giphy = await _context.Giphy.FindAsync(id);
//            if (giphy == null)
//            {
//                return null;
//            }

//            _context.Giphy.Remove(giphy);
//            await _context.SaveChangesAsync();

//            return giphy;
//        }
//        public bool GiphyExists(int id)
//        {
//            return _context.Giphy.Any(e => e.ID == id);
//        }
//    }
//}