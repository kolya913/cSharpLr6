using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service1.Models;
using System.Net.Http;
using System.Text.Json;
using log4net;
using log4net.Config;

namespace Service1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ClientsController));
        private readonly ClientContext _context;

        public ClientsController(ClientContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
                log.Info($"GET /api/Clients - Start Time: {DateTime.Now}");

                if (_context.Clients == null)
                {
                    log.Error("Not found");
                    log.Info($"GET /api/Clients - End Time: {DateTime.Now}");
                    return NotFound();
                }

                log.Info("Correctly");
                var clients = await _context.Clients.ToListAsync();

                log.Info($"GET /api/Clients - End Time: {DateTime.Now}");
                return clients;
            }
            catch (Exception ex)
            {
                log.Error($"GET /api/Clients - Exception: {ex.Message}");
                log.Info($"GET /api/Clients - End Time: {DateTime.Now}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            try
            {
                log.Info($"GET /api/Clients/{id} - Start Time: {DateTime.Now}");

                if (_context.Clients == null)
                {
                    log.Error("Not found");
                    log.Info($"GET /api/Clients/{id} - End Time: {DateTime.Now}");
                    return NotFound();
                }

                var client = await _context.Clients.FindAsync(id);

                if (client == null)
                {
                    log.Error("Not found");
                    log.Info($"GET /api/Clients/{id} - End Time: {DateTime.Now}");
                    return NotFound();
                }
                log.Info("Correctly");
                log.Info($"GET /api/Clients/{id} - End Time: {DateTime.Now}");
                return client;
            }
            catch (Exception ex)
            {
                log.Error($"GET /api/Clients/{id} - Exception: {ex.Message}");
                log.Info($"GET /api/Clients/{id} - End Time: {DateTime.Now}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, Client client)
        {
            try
            {
                log.Info($"PUT /api/Clients/{id} - Start Time: {DateTime.Now}");

                if (id != client.Id)
                {
                    log.Error("Not found");
                    log.Info($"PUT /api/Clients/{id} - End Time: {DateTime.Now}");
                    return BadRequest();
                }

                _context.Entry(client).State = EntityState.Modified;

                await _context.SaveChangesAsync();
                log.Info("Correctly");
                log.Info($"PUT /api/Clients/{id} - End Time: {DateTime.Now}");
                return NoContent();
            }
            catch (Exception ex)
            {
                log.Error($"PUT /api/Clients/{id} - Exception: {ex.Message}");
                log.Info($"PUT /api/Clients/{id} - End Time: {DateTime.Now}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            try
            {
                log.Info($"POST /api/Clients - Start Time: {DateTime.Now}");

                if (_context.Clients == null)
                {
                    log.Error("Not found");
                    log.Info($"POST /api/Clients - Start Time: {DateTime.Now}");
                    return Problem("Entity set 'ClientContext.Clients'  is null.");
                }

                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                log.Info("Correctly");
                log.Info($"POST /api/Clients - End Time: {DateTime.Now}");
                return CreatedAtAction("GetClient", new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
                log.Error($"POST /api/Clients - Exception: {ex.Message}");
                log.Info($"POST /api/Clients - Start Time: {DateTime.Now}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                log.Info($"DELETE /api/Clients/{id} - Start Time: {DateTime.Now}");

                if (_context.Clients == null)
                {
                    log.Error("Not found");
                    log.Info($"DELETE /api/Clients/{id} - Start Time: {DateTime.Now}");
                    return NotFound();
                }

                var client = await _context.Clients.FindAsync(id);

                if (client == null)
                {
                    log.Error("Not found");
                    log.Info($"DELETE /api/Clients/{id} - Start Time: {DateTime.Now}");
                    return NotFound();
                }

                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                log.Info("Correctly");
                log.Info($"DELETE /api/Clients/{id} - End Time: {DateTime.Now}");
                return NoContent();
            }
            catch (Exception ex)
            {
                log.Error($"DELETE /api/Clients/{id} - Exception: {ex.Message}");
                log.Info($"DELETE /api/Clients/{id} - Start Time: {DateTime.Now}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
