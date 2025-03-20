using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogfileApi.Models
{
    public class LogFileItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required DateTime Timestamp { get; set; }
        public required string Case { get; set; }
        public required string Type { get; set; }
        public required string Endpoint { get; set; }

        // Foreign key
        public int? User_ID { get; set; } // Make User_ID nullable
    }
}