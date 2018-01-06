using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UpwardApi.BasicBracket;
using UpwardApi.Data;
using UpwardApi.Models;

namespace UpwardApi.Controllers
{
    [Route("api/[controller]")]
    public class BrandController
    {
        private readonly CarContext _context;

        private readonly ILogger _logger;
        public BrandController(CarContext context, ILogger<BrandController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "");

            var list = await _context.Brands.Include(x => x.Serieses).ThenInclude(x => x.Models).AsNoTracking().ToListAsync();
            var result = new HttpResult(list);

            return new JsonResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "");

            var item = await _context.Brands.Include(x => x.Serieses).ThenInclude(x => x.Models).SingleOrDefaultAsync(x => x.BrandId == id);

            if (item == null)
            {
                return new JsonResult(HttpResult.NoFound);
            }

            return new JsonResult(new HttpResult(item));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}