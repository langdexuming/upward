using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reggie.Upward.WebApi.Areas.PlatformAccount.Data;
using Reggie.Upward.WebApi.Areas.ResourceManage.Models;

namespace Reggie.Upward.WebApi.Areas.ResourceManage.Controllers
{
    [Produces("application/json")]
    [Route("api/FileModels")]
    public class FileModelsController : Controller
    {
        private readonly PlatformAccountContext _context;

        public FileModelsController(PlatformAccountContext context)
        {
            _context = context;
        }

        // GET: api/FileModels
        [HttpGet]
        public IEnumerable<FileModel> GetFileModel()
        {
            return _context.FileModel;
        }

        // GET: api/FileModels/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFileModel([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fileModel = await _context.FileModel.SingleOrDefaultAsync(m => m.ID == id);

            if (fileModel == null)
            {
                return NotFound();
            }

            return Ok(fileModel);
        }

        // PUT: api/FileModels/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFileModel([FromRoute] string id, [FromBody] FileModel fileModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fileModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(fileModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FileModelExists(id))
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

        // POST: api/FileModels
        [HttpPost]
        public async Task<IActionResult> PostFileModel([FromBody] FileModel fileModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.FileModel.Add(fileModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFileModel", new { id = fileModel.ID }, fileModel);
        }

        // POST: api/FileModels
        [HttpPost]
        public async Task<IActionResult> PostFileModel(IFormFile files)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fileModel= new FileModel() {  };
            _context.FileModel.Add(fileModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFileModel", new { id = fileModel.ID }, fileModel);
        }


        // DELETE: api/FileModels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFileModel([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var fileModel = await _context.FileModel.SingleOrDefaultAsync(m => m.ID == id);
            if (fileModel == null)
            {
                return NotFound();
            }

            _context.FileModel.Remove(fileModel);
            await _context.SaveChangesAsync();

            return Ok(fileModel);
        }

        private bool FileModelExists(string id)
        {
            return _context.FileModel.Any(e => e.ID == id);
        }
    }
}