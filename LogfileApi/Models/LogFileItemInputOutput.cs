namespace LogfileApi.Models
{
    public class LogFileItemInputOutput
    {
        public DateTime Timestamp { get; set; }
        public string Case { get; set; }
        public string UserName { get; set; }
        public string Type { get; set; }
        public string Endpoint { get; set; }
    }
}

