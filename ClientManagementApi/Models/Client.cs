using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ClientManagementApi.Models
{
    public enum ClientType
    {
        LegalEntity,
        IndividualEntrepreneur
    }
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(12)]
        public string INN { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [Required]
        public ClientType Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public List<Founder> Founders { get; set; } = new();
    }
}
