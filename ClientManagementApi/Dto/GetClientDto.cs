using ClientManagementApi.Models;

namespace ClientManagementApi.Dto
{
    public class GetClientDto
    {
        public int Id { get; set; }
        public string INN { get; set; }
        public string Name { get; set; }
        public ClientType Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<GetFounderDto> Founders { get; set; }
    }
}
