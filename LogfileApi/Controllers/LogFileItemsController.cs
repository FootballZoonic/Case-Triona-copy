using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;
using LogfileApi.Services.LogFileControllerEndpoints;
using LogfileApi.Services.LogFileItemsControllerEndpoints;
using LogfileApi.Services.LogFileItemsControllerEndpoints.DevelopmentServices;

namespace LogfileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogFileItemsController : ControllerBase
    {
        private readonly LogFileContext _context;
        private readonly LogFileItemSearcher _logFileItemSearcher;
        private readonly LogFileItemPoster _logFileItemPoster;
        private readonly LogFileItemDeleter _logFileItemDeleter;

        public LogFileItemsController(LogFileContext context, LogFileItemSearcher logFileItemSearcher, LogFileItemPoster logFileItemPoster, LogFileItemDeleter logFileItemDeleter)
        {
            _context = context;
            _logFileItemSearcher = logFileItemSearcher;
            _logFileItemPoster = logFileItemPoster;
            _logFileItemDeleter = logFileItemDeleter;
        }

        // GET: api/LogFileItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogFileItem>>> GetLogFileItems()
        {
            return await _context.LogFileItems.ToListAsync();
        }

        // GET: api/LogFileItems/search?caseParam=abc
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<LogFileItemInputOutputActor>>> SearchLogFileItems([FromQuery] string? caseParam, string? userParam, string? actorParam, string? typeParam, DateTime? startDateParam, DateTime? endDateParam)
        {
            var logFileItems = await _logFileItemSearcher.SearchLogFileItems(caseParam, userParam, actorParam, typeParam, startDateParam, endDateParam);

            if (logFileItems == null || !logFileItems.Any())
            {
                return NotFound();
            }

            return Ok(logFileItems);
        }

        // POST: api/LogFileItems
        [HttpPost]
        public async Task<ActionResult> PostLogFileItems(List<LogFileItemInputOutput> logFileItemsInput)
        {
            var result = await _logFileItemPoster.PostLogFileItems(logFileItemsInput);

            if (result == "Log file items cannot be null or empty.")
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        // DELETE: api/LogFileItems
        [HttpDelete]
        public async Task<IActionResult> DeleteAllLogFileItems()
        {
            var result = await _logFileItemDeleter.DeleteAllLogFileItems();

            if (result == "No log file items found to delete.")
            {
                return NotFound(result);
            }

            return NoContent();
        }
    }
}