using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileTitlesControllerEndpoints
{
    public class LogFileTitleUpdater
    {
        private readonly LogFileContext _context;

        public LogFileTitleUpdater(LogFileContext context)
        {
            _context = context;
        }

        public async Task<bool> UpdateLogFileTitle(LogFileTitle logFileTitle)
        {
            if (logFileTitle == null || string.IsNullOrEmpty(logFileTitle.Title))
            {
                throw new ArgumentException("Invalid log file title data.");
            }

            var existingLogFileTitle = await _context.LogFileTitles
                .FirstOrDefaultAsync(l => l.Title == logFileTitle.Title);

            if (existingLogFileTitle == null)
            {
                return false;
            }

            existingLogFileTitle.NumOfRows = logFileTitle.NumOfRows;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}