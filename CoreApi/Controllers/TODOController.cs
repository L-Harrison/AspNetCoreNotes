using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using CoreApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TODOController : ControllerBase
    {
        private readonly MyContext _myContext;

        public TODOController(MyContext myContext)
        {
            _myContext = myContext;
            if (_myContext.Set<TODO>().Count() == 0)
            {
                _myContext.AddRange(new List<TODO>()
                {
                    new TODO()
                    {
                        Name = "asda",
                        IsComplete = true
                    },
                    new TODO()
                    {
                        Name = "asda_1",
                        IsComplete = true
                    },
                    new TODO()
                    {
                        Name = "asda_2",
                        IsComplete = false
                    }
                });
                _myContext.SaveChanges();
            }

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TODO>>> Get()
        {
            return await _myContext.Set<TODO>().ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TODO>> Get(int id)
        {
            var re = await _myContext.Set<TODO>().FindAsync(id);
            if (re == null)
            {
                return NotFound();
            }

            return re;
        }
        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<TODO>> Post([FromBody]TODO item)
        {
            _myContext.Set<TODO>().Add(item);
            await _myContext.SaveChangesAsync();

            return CreatedAtAction("Get", new { id = item.Id }, item);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<TODO>> Put(int id, TODO todo)
        {
            if (todo.Id != id)
            {
                return BadRequest();
            }

            _myContext.Entry(todo).State = EntityState.Modified;
            await _myContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var s = await _myContext.Set<TODO>().FindAsync(id);
            if (s != null)
            {
                var sp = _myContext.Remove(s);
                var seq = await _myContext.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}