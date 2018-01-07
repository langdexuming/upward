using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UpwardApi.BasicBracket;
using UpwardApi.Data;
using UpwardApi.Models;

namespace UpwardApi.Controllers
{
    [EnableCors("AllowSameDomain")]
    [Route("api/[controller]")]
    public class CarController : Controller
    {
        private readonly CarContext _context;

        private readonly ILogger _logger;

        public CarController(CarContext context, ILogger<CarController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "");

            var list = await _context.CarItems.AsNoTracking().ToListAsync();
            var result = new HttpResult(list);

            return new JsonResult(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, $"{id}");

            var item = await _context.CarItems.FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, $"{id}");
                return new JsonResult(HttpResult.NoFound);
            }

            return new JsonResult(new HttpResult(item));
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]CarItem item)
        {
            _logger.LogInformation(LoggingEvents.InsertItem, $"{new JsonResult(item)}");

            item.RegisterDateTime = DateTime.Now;

            try
            {
                _context.CarItems.Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(LoggingEvents.InsertItemException, $"{item.Id},eeception message:{ex}");
            }
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