using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogfileApi.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int User_ID { get; set; }
        public required string User_name { get; set; }

        // Foreign key
        public int Actor_ID { get; set; }
        // Navigation property
        public Actor Actor { get; set; }

        // Navigation property
        public ICollection<LogFileItem> LogFileItems { get; set; }
    }
}
