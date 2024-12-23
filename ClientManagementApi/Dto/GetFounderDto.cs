using ClientManagementApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientManagementApi.Dto
{
    public class GetFounderDto
    {
        public int Id { get; set; }
        public string INN { get; set; }
        public string FullName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
