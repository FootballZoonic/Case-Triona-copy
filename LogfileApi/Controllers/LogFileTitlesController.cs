using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;
using LogfileApi.Services.LogFileTitlesControllerEndpoints;
using LogfileApi.Services.LogFileTitlesControllerEndpoints.DevelopmentServices;

namespace LogfileApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogFileTitlesController : ControllerBase
    {
        private readonly LogFileContext _context;
        private readonly LogFileTitleSearcher _logFileTitleSearcher;
        private readonly LogFileTitleRowCounter _logFileTitleRowCounter;
        private readonly LogFileTitlePoster _logFileTitlePoster;
        private readonly LogFileTitleDeleter _logFileTitleDeleter;
        private readonly LogFileTitleGetter _logFileTitleGetter;
        private readonly LogFileTitleUpdater _logFileTitleUpdater;

        public LogFileTitlesController(LogFileContext context, LogFileTitleSearcher logFileTitleSearcher, LogFileTitleRowCounter logFileTitleRowCounter, LogFileTitlePoster logFileTitlePoster, LogFileTitleDeleter logFileTitleDeleter, LogFileTitleGetter logFileTitleGetter, LogFileTitleUpdater logFileTitleUpdater)
        {
            _context = context;
            _logFileTitleSearcher = logFileTitleSearcher;
            _logFileTitleRowCounter = logFileTitleRowCounter;
            _logFileTitlePoster = logFileTitlePoster;
            _logFileTitleDeleter = logFileTitleDeleter;
            _logFileTitleGetter = logFileTitleGetter;
            _logFileTitleUpdater = logFileTitleUpdater;
        }

        // GET: api/LogFileTitles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogFileTitle>>> GetLogFileTitles()
        {
            return await _context.LogFileTitles.ToListAsync();
        }

        // GET: api/LogFileTitles/search?titleParam=abc
        [HttpGet("search")]
        public async Task<ActionResult<bool>> SearchLogFileTitles([FromQuery] string? titleParam)
        {
            try
            {
                var exists = await _logFileTitleSearcher.SearchLogFileTitles(titleParam);
                return Ok(exists);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/LogFileTitles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogFileTitle>> GetLogFileTitle(int id)
        {
            var logFileTitle = await _logFileTitleGetter.GetLogFileTitle(id);

            if (logFileTitle == null)
            {
                return NotFound();
            }

            return logFileTitle;
        }

        // GET: api/LogFileTitles/rows?titleParam=abc
        [HttpGet("rows")]
        public async Task<ActionResult<int>> GetLogFileTitleRows([FromQuery] string? titleParam)
        {
            try
            {
                var numOfRows = await _logFileTitleRowCounter.GetLogFileTitleRows(titleParam);
                return Ok(numOfRows);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/LogFileTitles
        [HttpPost]
        public async Task<ActionResult<LogFileTitle>> PostLogFileTitle(LogFileTitle logFileTitle)
        {
            var createdLogFileTitle = await _logFileTitlePoster.PostLogFileTitle(logFileTitle);
            return CreatedAtAction(nameof(GetLogFileTitle), new { id = createdLogFileTitle.Id }, createdLogFileTitle);
        }

        // PUT: api/LogFileTitles
        [HttpPut]
        public async Task<IActionResult> UpdateLogFileTitle([FromBody] LogFileTitle logFileTitle)
        {
            try
            {
                var updated = await _logFileTitleUpdater.UpdateLogFileTitle(logFileTitle);
                if (!updated)
                {
                    return NotFound("Log file title not found.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/LogFileTitles
        [HttpDelete]
        public async Task<IActionResult> DeleteAllLogFileTitles()
        {
            var result = await _logFileTitleDeleter.DeleteAllLogFileTitles();

            if (result == "No log file titles found to delete.")
            {
                return NotFound(result);
            }

            return NoContent();
        }

        private bool LogFileTitleExists(int id)
        {
            return _context.LogFileTitles.Any(e => e.Id == id);
        }
    }
}