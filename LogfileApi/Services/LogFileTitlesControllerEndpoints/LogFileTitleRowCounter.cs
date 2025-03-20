using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileTitlesControllerEndpoints
{
    public class LogFileTitleRowCounter
    {
        private readonly LogFileContext _context;

        public LogFileTitleRowCounter(LogFileContext context)
        {
            _context = context;
        }

        public async Task<int> GetLogFileTitleRows(string? titleParam)
        {
            if (string.IsNullOrEmpty(titleParam))
            {
                throw new ArgumentException("Title parameter cannot be null or empty.");
            }

            var logFileTitle = await _context.LogFileTitles
                .FirstOrDefaultAsync(l => l.Title == titleParam);

            if (logFileTitle == null)
            {
                throw new KeyNotFoundException("Log file title not found.");
            }

            return logFileTitle.NumOfRows;
        }
    }
}