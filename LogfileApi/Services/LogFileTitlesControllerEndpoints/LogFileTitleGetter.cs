using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileTitlesControllerEndpoints
{
    public class LogFileTitleGetter
    {
        private readonly LogFileContext _context;

        public LogFileTitleGetter(LogFileContext context)
        {
            _context = context;
        }

        public async Task<LogFileTitle?> GetLogFileTitle(int id)
        {
            var logFileTitle = await _context.LogFileTitles.FindAsync(id);
            return logFileTitle;
        }
    }
}