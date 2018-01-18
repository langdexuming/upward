using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Reggie.Upward.WebApi.BasicBracket;
using Reggie.Upward.WebApi.Data;
using Reggie.Upward.WebApi.Models;

namespace Reggie.Upward.WebApi.Controllers
{
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]")]
    public class BrandController : Controller
    {
        private readonly CarContext _context;

        private readonly ILogger _logger;
        public BrandController(CarContext context, ILogger<BrandController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/brand
        [HttpGet]
        public IEnumerable<Brand> GetAll()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "");

            return _context.Brands.Include(x => x.Serieses).ThenInclude(x => x.Models).ToList();
        }

        // GET api/brand/5
        [HttpGet("{id}", Name = "GetBrand")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, $"{id}");

            var item = _context.Brands.Include(x => x.Serieses).ThenInclude(x => x.Models).FirstOrDefault(x => x.BrandId == id);

            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
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