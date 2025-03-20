using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileTitlesControllerEndpoints
{
    public class LogFileTitlePoster
    {
        private readonly LogFileContext _context;

        public LogFileTitlePoster(LogFileContext context)
        {
            _context = context;
        }

        public async Task<LogFileTitle> PostLogFileTitle(LogFileTitle logFileTitle)
        {
            _context.LogFileTitles.Add(logFileTitle);
            await _context.SaveChangesAsync();
            return logFileTitle;
        }
    }
}