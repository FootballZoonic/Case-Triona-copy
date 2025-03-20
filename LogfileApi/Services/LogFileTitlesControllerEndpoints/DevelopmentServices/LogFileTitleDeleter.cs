using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileTitlesControllerEndpoints.DevelopmentServices
{
    public class LogFileTitleDeleter
    {
        private readonly LogFileContext _context;

        public LogFileTitleDeleter(LogFileContext context)
        {
            _context = context;
        }

        public async Task<string> DeleteAllLogFileTitles()
        {
            var logFileTitles = await _context.LogFileTitles.ToListAsync();
            if (logFileTitles == null || logFileTitles.Count == 0)
            {
                return "No log file titles found to delete.";
            }

            _context.LogFileTitles.RemoveRange(logFileTitles);
            await _context.SaveChangesAsync();

            return "Log file titles deleted successfully.";
        }
    }
}