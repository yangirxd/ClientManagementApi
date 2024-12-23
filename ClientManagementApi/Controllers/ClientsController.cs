using ClientManagementApi.Data;
using ClientManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClientManagementApi.Dto;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Xml.Linq;

namespace ClientManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly ClientContext _context;

        public ClientsController(ClientContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _context.Clients
            .Include(c => c.Founders)
            .Select(c => new GetClientDto
            {
                Id = c.Id,
                INN = c.INN,
                Name = c.Name,
                Type = c.Type,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Founders = c.Founders.Select(f => new GetFounderDto
                {
                    Id = f.Id,
                    INN = f.INN,
                    FullName = f.FullName,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt,
                }).ToList()
            })
            .ToListAsync();

            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _context.Clients
            .Include(c => c.Founders)
            .Select(c => new GetClientDto
            {
                Id = c.Id,
                INN = c.INN,
                Name = c.Name,
                Type = c.Type,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt,
                Founders = c.Founders.Select(f => new GetFounderDto
                {
                    Id = f.Id,
                    INN = f.INN,
                    FullName = f.FullName,
                    CreatedAt = f.CreatedAt,
                    UpdatedAt = f.UpdatedAt,
                }).ToList()
            }).FirstOrDefaultAsync(x => x.Id == id);

            if (client == null) return NotFound();

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] AddClientDto addClientDto)
        {
            // Проверка на бизнес-правило
            if (addClientDto.Type == ClientType.IndividualEntrepreneur && addClientDto.Founders.Any())
            {
                return BadRequest("Individual entrepreneurs cannot have founders.");
            }

            var client = new Client()
            {
                INN = addClientDto.INN,
                Name = addClientDto.Name,
                Type = addClientDto.Type,
                Founders = addClientDto.Founders.Select(x => new Founder() { INN = x.INN, FullName = x.FullName}).ToList(),
            };

            // Сохранение клиента в базе данных
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] UpdateClientDto updateClientDto)
        {
            if (updateClientDto.Type == ClientType.IndividualEntrepreneur && updateClientDto.Founders.Any())
            {
                return BadRequest("Individual entrepreneurs cannot have founders.");
            }

            var client = await _context.Clients.Include(c => c.Founders).FirstOrDefaultAsync(x => x.Id == updateClientDto.Id);

            if (client == null) return BadRequest();

            client.INN = updateClientDto.INN;
            client.Name = updateClientDto.Name;
            client.Type = updateClientDto.Type;
            client.UpdatedAt = DateTime.UtcNow;
            foreach (var item in updateClientDto.Founders) {
                var founder = client.Founders.FirstOrDefault(x => x.Id == item.Id);
                if (founder == null)
                {
                    client.Founders.Add(new Founder() { INN = item.INN, FullName = item.FullName});
                    continue;
                }
                founder.INN = item.INN;
                founder.FullName = item.FullName;
                founder.UpdatedAt = DateTime.UtcNow;
            }

            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return NotFound();

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
