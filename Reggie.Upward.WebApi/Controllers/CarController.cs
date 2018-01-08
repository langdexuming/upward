using System;
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
    public class CarController : Controller
    {
        private readonly CarContext _context;

        private readonly ILogger _logger;

        public CarController(CarContext context, ILogger<CarController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET api/car
        [HttpGet]
        public IEnumerable<CarItem> GetAll()
        {
            _logger.LogInformation(LoggingEvents.ListItems, "");

            return _context.CarItems.ToList();
        }

        // GET api/car/5
        [HttpGet("{id}", Name = "GetCarItem")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation(LoggingEvents.GetItem, $"{id}");

            var item = _context.CarItems.FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            return new ObjectResult(item);
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]CarItem item)
        {
            _logger.LogInformation(LoggingEvents.InsertItem, $"{new JsonResult(item)}");

            if (item == null)
            {
                return BadRequest();
            }

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

            return CreatedAtRoute("GetCarItem", new { id = item.Id }, item);
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