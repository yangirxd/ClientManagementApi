using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientManagementApi.Models
{
    public class Founder
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string INN { get; set; }

        [Required]
        [StringLength(200)]
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

}
