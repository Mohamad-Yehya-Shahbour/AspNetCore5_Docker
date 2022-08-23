using colours.Data;
using colours.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace colours.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ColoursController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ColoursController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<ColoursController>
        [HttpGet]
        public IEnumerable<Colour> GetAll()
        {
            return _context.Colours;
        }

        // GET api/<ColoursController>/5
        [HttpGet("{id}")]
        public IActionResult GetColour(int id)
        {
            return Ok(_context.Colours.Where(x => x.Id == id).FirstOrDefault()) ;
        }

        //POST api/<ColoursController>
        [HttpPost]
        public void Add([FromBody] Colour color)
        {
            _context.Colours.Add(color);
            _context.SaveChanges();
        }

        //// PUT api/<ColoursController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<ColoursController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
