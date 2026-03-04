using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var comments = await _context.Comments
                    .Include(c => c.RegisterUser)
                    .Include(c => c.Ticket)
                    .Select(c => new CommentsDto
                    {
                        Id_Comentario = c.CommentId,
                        Comentario = c.Comentario,
                        FechaComentario = c.CommentDate,
                        NameUserComment = c.NameUserComment,
                        RegistroId = c.RegisterUser.UserRecordId,
                        Id_Ticket = c.TicketId
                    })
                    .ToListAsync();

                return Ok(comments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting comments", error = ex.Message });
            }
        }

        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> Create([FromBody] CreateCommentDto dto)
        {
            try
            {
                if (dto == null || string.IsNullOrWhiteSpace(dto.Comentario))
                    return BadRequest(new { message = "The comment cannot be empty.." });

                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("Unauthenticated user.");

                var comment = new Comment
                {
                    TicketId = dto.Id_Ticket,
                    Comentario = dto.Comentario,
                    NameUserComment = dto.NameUserComment,
                    UserRecordId = int.Parse(userId),
                    CommentDate = DateTime.UtcNow
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Comment added", comment.CommentId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating comment", error = ex.Message });
            }
        }

        [HttpGet]
        [Route("GetByIdComments/{Id}")]

        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var comentario = await _context.Comments
                    .Include(c => c.UserRecordId)
                    .FirstOrDefaultAsync(c => c.CommentId == id);

                if (comentario == null)
                    return NotFound(new { message = "Comment not found." });

                return Ok(new CommentsDto
                {
                    Id_Comentario = comentario.CommentId,
                    Comentario = comentario.Comentario,
                    FechaComentario = comentario.CommentDate,
                    NameUserComment = comentario.NameUserComment,
                    Username = comentario.RegisterUser.Username,
                    Id_Ticket = comentario.TicketId
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting the comment", error = ex.Message });
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] string comentarioNuevo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(comentarioNuevo))
                    return BadRequest(new { message = "The comment cannot be empty." });

                var comentario = await _context.Comments.FindAsync(id);
                if (comentario == null)
                    return NotFound(new { message = "Comment not found." });

                comentario.Comentario = comentarioNuevo;
                await _context.SaveChangesAsync();

                return Ok(new { message = "Updated commentary." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating comment", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var comentario = await _context.Comments.FindAsync(id);
                if (comentario == null)
                    return NotFound(new { message = "Comment not found." });

                _context.Comments.Remove(comentario);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Comment removed" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting comment", error = ex.Message });
            }
        }
    }

}
