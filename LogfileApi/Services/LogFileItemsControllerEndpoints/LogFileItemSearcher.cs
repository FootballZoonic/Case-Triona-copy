using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LogfileApi.Models;

namespace LogfileApi.Services.LogFileControllerEndpoints
{
    public class LogFileItemSearcher
    {
        private readonly LogFileContext _context;

        public LogFileItemSearcher(LogFileContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogFileItemInputOutputActor>> SearchLogFileItems(string? caseParam, string? userParam, string? actorParam, string? typeParam, DateTime? startDateParam, DateTime? endDateParam)
        {
            var query = _context.LogFileItems.AsQueryable();

            if (!string.IsNullOrEmpty(caseParam))
            {
                query = query.Where(l => l.Case == caseParam);
            }

            if (!string.IsNullOrEmpty(userParam))
            {
                var usersList = await _context.Users.ToListAsync();
                var usersLookup = usersList.ToLookup(u => u.User_name, u => u.User_ID);
                int? userID = usersLookup[userParam].FirstOrDefault();

                if (userID != 0)
                {
                    query = query.Where(l => l.User_ID == userID);
                }
                else
                {
                    return Enumerable.Empty<LogFileItemInputOutputActor>();
                }
            }

            if (!string.IsNullOrEmpty(actorParam))
            {
                var actorsLists = await _context.Actors.ToListAsync();
                var actorsLookups = actorsLists.ToLookup(a => a.Actor_name, a => a.Actor_ID);
                int? actorID = actorsLookups[actorParam].FirstOrDefault();

                if (actorID != 0)
                {
                    var usersWithActor = await _context.Users.Where(u => u.Actor_ID == actorID).ToListAsync();
                    var userIDsWithActor = usersWithActor.Select(u => u.User_ID).ToList();
                    query = query.Where(l => userIDsWithActor.Contains((int)l.User_ID));
                }
                else
                {
                    return Enumerable.Empty<LogFileItemInputOutputActor>();
                }
            }

            if (!string.IsNullOrEmpty(typeParam))
            {
                query = query.Where(l => l.Type == typeParam);
            }

            if (startDateParam.HasValue && endDateParam.HasValue)
            {
                query = query.Where(l => l.Timestamp >= startDateParam.Value && l.Timestamp <= endDateParam.Value);
            }
            else if (startDateParam.HasValue)
            {
                query = query.Where(l => l.Timestamp >= startDateParam.Value);
            }
            else if (endDateParam.HasValue)
            {
                query = query.Where(l => l.Timestamp <= endDateParam.Value);
            }

            var logFileItems = await query.ToListAsync();

            if (logFileItems == null || logFileItems.Count == 0)
            {
                return Enumerable.Empty<LogFileItemInputOutputActor>();
            }

            // Fetch all users to map user IDs to user names
            var allUsers = await _context.Users.ToDictionaryAsync(u => u.User_ID, u => u.User_name);
            var allUsersActor = await _context.Users.ToDictionaryAsync(u => u.User_ID, u => u.Actor_ID);

            // Fetch all actors to map actor IDs to actor names
            var actorsList = await _context.Actors.ToListAsync();
            var actorsLookup = actorsList.ToLookup(a => a.Actor_ID, a => a.Actor_name);

            var logFileItemOutputs = logFileItems.Select(l => new LogFileItemInputOutputActor
            {
                Timestamp = l.Timestamp,
                Case = l.Case,
                UserName = l.User_ID.HasValue && allUsers.ContainsKey(l.User_ID.Value) ? allUsers[l.User_ID.Value] : string.Empty,
                Actor = l.User_ID.HasValue && allUsersActor.ContainsKey(l.User_ID.Value) && actorsLookup.Contains(allUsersActor[l.User_ID.Value]) ? actorsLookup[allUsersActor[l.User_ID.Value]].FirstOrDefault() : string.Empty,
                Type = l.Type,
                Endpoint = l.Endpoint
            }).ToList();

            return logFileItemOutputs;
        }
    }
}