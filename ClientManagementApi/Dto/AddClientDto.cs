using ClientManagementApi.Models;

namespace ClientManagementApi.Dto
{
    public class AddClientDto
    {
        public string INN { get; set; }
        public string Name { get; set; }
        public ClientType Type { get; set; }
        public List<AddFounderDto> Founders { get; set; } = new();
    }
}
