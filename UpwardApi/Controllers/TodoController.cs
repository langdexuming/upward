using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UpwardApi.Models;

namespace UpwardApi.Controllers
{
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        private readonly ILogger _logger;

        public TodoController(TodoContext todoContext, ILogger<TodoController> logger)
        {
            _context = todoContext;
            _logger = logger;

            _logger.LogInformation(LoggingEvents.GenerateItems, "GenerateItems", "");
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }

            _logger.LogDebug(LoggingEvents.GetItem, "Get", "");

            //_logger.LogDebug(LoggingEvents.GetItem, "Get({ID}) NOT FOUND", id);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "todo";
        }
    }
}