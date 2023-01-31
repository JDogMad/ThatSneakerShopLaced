using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ThatSneakerShopLaced.Models;
using ThatSneakerShopLaced.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using ThatSneakerShopLaced.Models.ViewModels;

namespace ThatSneakerShopLaced.Api.Controllers
{

    [Authorize(Roles = "Manager, Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StockManagmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StockManagmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StockManagment/Shoes
        [HttpGet("Shoes")]
        public async Task<IActionResult> GetAllShoes()
        {
            var stock = await _context.Shoe.ToListAsync();
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        // GET: api/StockManagment/Shoes/5
        [HttpGet("Shoes/{id}")]
        public async Task<IActionResult> GetShoeById(int id)
        {
            var stock = await _context.Shoe.FindAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        // GET: api/StockManagment/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var stock = await _context.Shoe.ToListAsync();
            return View(stock);
        }



        // POST: api/StockManagment/Shoes
        [HttpPost("Shoes")]
        public async Task<IActionResult> CreateShoe([FromBody] Shoe shoe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Shoe.Add(shoe);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetShoeById", new { id = shoe.ShoeId }, shoe);
        }

        // PUT: api/StockManagment/Shoes/5
        [HttpPut("Shoes/{id}")]
        public async Task<IActionResult> UpdateShoe(int id, [FromBody] Shoe shoe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shoe.ShoeId)
            {
                return BadRequest();
            }

            _context.Entry(shoe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShoeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/StockManagment/Shoes/5
        [HttpDelete("Shoes/{id}")]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            var shoe = await _context.Shoe.FindAsync(id);
            if (shoe == null)
            {
                return NotFound();
            }

            _context.Shoe.Remove(shoe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShoeExists(int id)
        {
            return _context.Shoe.Any(e => e.ShoeId == id);
        }
    }
}
