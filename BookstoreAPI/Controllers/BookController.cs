using System.Threading.Tasks;
using BookstoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookstoreAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookStoreContext _context;
        public BookController(BookStoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var data = await _context.TblBook.ToListAsync();
            return Ok(data);
        }
    }
}