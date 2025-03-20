using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileTitlesControllerEndpoints
{
    public class LogFileTitleSearcher
    {
        private readonly LogFileContext _context;

        public LogFileTitleSearcher(LogFileContext context)
        {
            _context = context;
        }

        public async Task<bool> SearchLogFileTitles(string? titleParam)
        {
            if (string.IsNullOrEmpty(titleParam))
            {
                throw new ArgumentException("Title parameter cannot be null or empty.");
            }

            var exists = await _context.LogFileTitles.AnyAsync(l => l.Title == titleParam);
            return exists;
        }
    }
}