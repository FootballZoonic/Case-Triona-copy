using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileItemsControllerEndpoints.DevelopmentServices
{
    public class LogFileItemDeleter
    {
        private readonly LogFileContext _context;

        public LogFileItemDeleter(LogFileContext context)
        {
            _context = context;
        }

        public async Task<string> DeleteAllLogFileItems()
        {
            var logFileItems = await _context.LogFileItems.ToListAsync();
            if (logFileItems == null || logFileItems.Count == 0)
            {
                return "No log file items found to delete.";
            }

            _context.LogFileItems.RemoveRange(logFileItems);
            await _context.SaveChangesAsync();

            return "Log file items deleted successfully.";
        }

        public bool LogFileItemExists(int id)
        {
            return _context.LogFileItems.Any(e => e.Id == id);
        }
    }
}