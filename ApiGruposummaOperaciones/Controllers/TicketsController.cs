using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System.Net.Sockets;
using System.Security.Claims;


namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var tickets = await _context.Tickets
                    .Include(t => t.TicketStatus) // Relación con StatusTicket
                    .Include(t => t.registerUser)
                    .Include(t => t.Comments)
                    .Select(t => new TicketDto
                    {
                        Id_Tickets = t.Id_Tickets,
                        Descripcion = t.Descripcion,
                        CreatedDate = t.CreatedDate,
                        ClosedDate = t.ClosedDate,
                        TicketStatusId = t.TicketStatusId,
                        UserRecordId = t.UserRecordId,
                        NameUserTicket = t.NameUserTicket,
                        UserName = t.registerUser.Username, // ajusta según tu propiedad real
                        Comments = t.Comments.Select(c => new CommentsDto
                        {
                            Id_Comentario = c.CommentId,
                            Comentario = c.Comentario,
                            FechaComentario = c.CommentDate,
                            RegistroId = c.UserRecordId
                        }).ToList(),
                        // Ahora asignamos la lista de StatusTicketDto
                        StatusTickets = t.TicketStatus != null ? new List<StatusTicketDto>
                        {
                    new StatusTicketDto
                    {
                        Descripcion = t.TicketStatus.Descripcion
                    }
                        } : new List<StatusTicketDto>() // En caso de que TicketStatus sea nulo
                    })
                    .ToListAsync();

                if (tickets == null || tickets.Count == 0)
                {
                    return NotFound(new { message = "No tickets found." });
                }

                return Ok(tickets);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving tickets.", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto ticketDto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userIdClaim))
                    return Unauthorized("The user could not be validated.");

                int userId = int.Parse(userIdClaim);

                var nuevoTicket = new Tickets
                {
                    Descripcion = ticketDto.Descripcion,
                    CreatedDate = DateTime.UtcNow,
                    ClosedDate = ticketDto.ClosedDate,
                    Id_EstatusOperacion = ticketDto.Id_EstatusOperacion,
                    UserRecordId = userId,
                    TicketStatusId = ticketDto.TicketStatusId,
                    NameUserTicket = ticketDto.NameUserTicket
                };

                _context.Tickets.Add(nuevoTicket);
                await _context.SaveChangesAsync();

                // Asignar el ticket a la operación correspondiente
                var operacion = await _context.Operations
                    .FirstOrDefaultAsync(o => o.Id_Operaciones == ticketDto.Id_Operacion);

                if (operacion == null)
                {
                    return NotFound(new { message = "No operation found to relate to the ticket." });
                }


                operacion.TicketId = nuevoTicket.Id_Tickets;
                await _context.SaveChangesAsync();

                // Cargar descripciones desde relaciones
                var ticketConDetalles = await _context.Tickets
                    .Include(t => t.TicketStatus)
                    .Include(t => t.Statusoperations)
                    .Include(t => t.registerUser)
                    .FirstOrDefaultAsync(t => t.Id_Tickets == nuevoTicket.Id_Tickets);
                if (ticketConDetalles == null)
                {
                    return NotFound(new { message = "Ticket created but related information could not be retrieved." });
                }

                return Ok(new
                {
                    message = "Ticket successfully created",
                    TicketId = ticketConDetalles.Id_Tickets,
                    Descripcion = ticketConDetalles.Descripcion,
                    FechaCreacion = ticketConDetalles.CreatedDate,
                    FechaCierre = ticketConDetalles.ClosedDate,
                    EstatusOperacion = ticketConDetalles.Statusoperations?.Description,
                    EstatusTicket = ticketConDetalles.TicketStatus?.Descripcion,
                    Usuario = ticketConDetalles.registerUser?.Username
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating ticket", error = ex.Message });
            }
        }


        [HttpPut]
        [Route("update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTicketDto updateDto)
        {
            try
            {
                var ticket = await _context.Tickets
                    .Include(t => t.TicketStatus)
                    .Include(t => t.Statusoperations)
                    .Include(t => t.registerUser)
                    .FirstOrDefaultAsync(t => t.Id_Tickets == id);
                if (ticket == null)
                {
                    return NotFound(new { message = "Ticket not found" });
                }

                // only update date of close and state of ticket
                ticket.ClosedDate = updateDto.CloseDate;
                ticket.TicketStatusId = updateDto.Id_StatusTicket;


                await _context.SaveChangesAsync();

                return Ok(new
                {
                    message = "Ticket updated correctly",
                    TicketId = ticket.Id_Tickets,
                    Descripcion = ticket.Descripcion,
                    FechaCreacion = ticket.CreatedDate,
                    FechaCierre = ticket.ClosedDate,
                    EstatusOperacion = ticket.Statusoperations?.Description,
                    EstatusTicket = ticket.TicketStatus?.Id_StatusTicket,
                    Description = ticket.TicketStatus?.Descripcion,
                    Usuario = ticket.registerUser?.Username
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating the ticket", error = ex.Message });
            }
        }


        [HttpGet]
        [Route("getbyid/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var ticket = await _context.Tickets
                    .Include(t => t.TicketStatus)
                    .Include(t => t.Statusoperations)
                    .Include(t => t.registerUser)
                    .Include(t => t.Comments)
                    .FirstOrDefaultAsync(t => t.Id_Tickets == id);

                if (ticket == null)
                {
                    return NotFound(new { message = "Ticket not found." });
                }

                var ticketDto = new TicketDto
                {
                    Id_Tickets = ticket.Id_Tickets,
                    Descripcion = ticket.Descripcion,
                    CreatedDate = ticket.CreatedDate,
                    ClosedDate = ticket.ClosedDate,
                    TicketStatusId = ticket.TicketStatusId,
                    UserRecordId = ticket.UserRecordId,
                    NameUserTicket = ticket.NameUserTicket,
                    UserName = ticket.registerUser?.Username,
                    Comments = ticket.Comments.Select(c => new CommentsDto
                    {
                        Id_Comentario = c.CommentId,
                        Comentario = c.Comentario,
                        FechaComentario = c.CommentDate,
                        RegistroId = c.UserRecordId,
                        NameUserComment = c.NameUserComment,
                    }).ToList(),
                    StatusTickets = ticket.TicketStatus != null ? new List<StatusTicketDto>
            {
                new StatusTicketDto { Descripcion = ticket.TicketStatus.Descripcion }
            } : new List<StatusTicketDto>()
                };

                return Ok(ticketDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error obtaining ticket", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var ticket = await _context.Tickets.FindAsync(id);

                if (ticket == null)
                {
                    return NotFound(new { message = "Ticket not found." });
                }

                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Ticket successfully deleted." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting ticket", error = ex.Message });
            }
        }

    }
}



