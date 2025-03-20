using LogFileBatchUploader.Models;

namespace LogFileBatchUploader.Services
{
    public static class LineParser
    {
        public static LogFileItem? ParseLine(string line)
        {
            var parts = line.Split('|');


            if (parts.Length != 6)
            {
                return null;
            }

            for (int i = 0; i < parts.Length; i++)
            {
                parts[i] = parts[i].Trim();
            }


            var retVal = new LogFileItem
            {
                Timestamp = DateTime.Parse(parts[0]),
                Case = parts[1],
                UserName = parts[2].ToUpper(),
                Type = parts[3],
                Endpoint = parts[4]
            };

            return retVal;
        }
            
    }
}

