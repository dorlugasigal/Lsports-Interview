using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using lsport.Handlers;
using lsport.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lsport.Models;

namespace lsport.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public partial class GiphyController : Controller
    {
        private readonly IManager _manager;

        public GiphyController(IManager manager)
        {
            _manager = manager;
        }

        // GET: api/Giphy
        [HttpGet]
        public ActionResult<List<Giphy>> GetGiphy()
        {
            return _manager.GetAll();
        }

        // GET: api/Giphy/5
        [HttpGet("{id}")]
        public ActionResult<Giphy> GetGiphy(int id)
        {
            var giphy = _manager.Get(id);
            return giphy ?? (ActionResult<Giphy>)NotFound();
        }

        // PUT: api/Giphy/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGiphy(int id, [FromBody]PostRequest item)
        {
            var ret = await _manager.Update(id, item.name);

            return ret switch
            {
                ActionResultEnum.BadRequest => BadRequest(),
                ActionResultEnum.NotFound => NotFound(),
                ActionResultEnum.NoContent => NoContent(),
                ActionResultEnum.Success => Ok(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        // POST: api/Giphy
        [HttpPost]
        public async Task<ActionResult<Giphy>> PostGiphy([FromBody]PostRequest item)
        {
            var ret = await _manager.Add(item.name);
            return CreatedAtAction($"GetGiphy", new { id = ret.ID }, ret);
        }

        // DELETE: api/Giphy/5
        [HttpDelete("{id}")]
        public ActionResult<Giphy> DeleteGiphy(int id)
        {
            var giphy = _manager.Delete(id);
            return giphy ?? (ActionResult<Giphy>)NotFound();
        }

    }
}
