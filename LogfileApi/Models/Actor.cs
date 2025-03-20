using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LogfileApi.Models
{
    public class Actor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Actor_ID { get; set; }
        public required string Actor_name { get; set; }
        public int Actor_type { get; set; }

        // Navigation property
        public ICollection<User> Users { get; set; }
    }
}

