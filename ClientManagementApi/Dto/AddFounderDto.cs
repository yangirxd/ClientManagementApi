using ClientManagementApi.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ClientManagementApi.Dto
{
    public class AddFounderDto
    {
        public string INN { get; set; }

        public string FullName { get; set; }
    }
}
