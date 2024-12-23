using ClientManagementApi.Models;

namespace ClientManagementApi.Dto
{
    public class UpdateClientDto
    {
        public int Id { get; set; }
        public string INN { get; set; }
        public string Name { get; set; }
        public ClientType Type { get; set; }
        public List<UpdateFounderDto> Founders { get; set; } = new();
    }
}
