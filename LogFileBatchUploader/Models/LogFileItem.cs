namespace LogFileBatchUploader.Models
{
    public class LogFileItem
    {
        public required DateTime Timestamp { get; set; }
        public required string Case { get; set; }
        public required string UserName { get; set; }
        public required string Type { get; set; }
        public required string Endpoint { get; set; }
    }
}