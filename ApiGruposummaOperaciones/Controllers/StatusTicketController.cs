using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using ApiGruposummaOperaciones.ModelsDto;

namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StatusTicketController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StatusTicketController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("StatusTicketGetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var statusTicket = await _context.StatusTickets
                    .Select(s => new DescriptionStatusTicketDto
                    {
                        Id_StatusTicket = s.Id_StatusTicket,
                        Description = s.Descripcion
                    })
                    .ToListAsync();
                return Ok(statusTicket);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = $"Error getting status ticket: {ex.Message}" });
            }
        }

    }
}
