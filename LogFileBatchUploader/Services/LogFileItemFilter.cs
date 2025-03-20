using LogFileBatchUploader.Models;

namespace LogFileBatchUploader.Services
{
    public static class LogFileItemFilter
    {
        public static bool Include(LogFileItem item)
        {
            return !item.Endpoint.ToLower().Contains("integration") &&
                   !item.UserName.ToLower().Contains("bkg_job") &&
                   !item.UserName.ToLower().Contains("anonymous");
        }
    }
}