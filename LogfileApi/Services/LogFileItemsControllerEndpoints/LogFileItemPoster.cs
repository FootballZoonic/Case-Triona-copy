using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileItemsControllerEndpoints
{
    public class LogFileItemPoster
    {
        private readonly LogFileContext _context;

        public LogFileItemPoster(LogFileContext context)
        {
            _context = context;
        }

        public async Task<string> PostLogFileItems(List<LogFileItemInputOutput> logFileItemsInput)
        {
            if (logFileItemsInput == null || !logFileItemsInput.Any())
            {
                return "Log file items cannot be null or empty.";
            }

            // Fetch all users once and store in a lookup
            var usersList = await _context.Users.ToListAsync();
            var usersLookup = usersList.ToLookup(u => u.User_name, u => u.User_ID);

            var logFileItems = new List<LogFileItem>();

            foreach (var input in logFileItemsInput)
            {
                int? userID = usersLookup[input.UserName].FirstOrDefault();

                if (userID == 0)
                {
                    userID = null;
                }

                var logFileItem = new LogFileItem
                {
                    Timestamp = input.Timestamp,
                    Case = input.Case,
                    Type = input.Type,
                    Endpoint = input.Endpoint,
                    User_ID = userID // Set User_ID to null if user is not found
                };

                logFileItems.Add(logFileItem);
            }

            _context.LogFileItems.AddRange(logFileItems);
            await _context.SaveChangesAsync();

            return "Log file items posted successfully.";
        }
    }
}