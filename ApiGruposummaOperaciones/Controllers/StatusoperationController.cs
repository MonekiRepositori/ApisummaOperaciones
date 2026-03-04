using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Authorization;


namespace ApiGruposummaOperaciones.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StatusoperationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public object StoredProcedures { get; private set; }

        public StatusoperationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("StatusOperationGetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {

                var statusoperation = await _context.StatusOperations.ToListAsync();
                return Ok(statusoperation);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { message = $"Error getting status operation: {ex.Message}" });
            }
        }

        // Obtains  the status of the operations by Id
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var statusoperation = await _context.StatusOperations.FindAsync(id);
                if (statusoperation == null)
                    return NotFound(new { message = "Register not found" });

                return Ok(statusoperation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting record", error = ex.Message });
            }
        }

        //creat new register 
        [HttpPost("CreateStatusOperation")]
        public async Task<IActionResult> Create([FromBody] StatusOperation statusoperation)
        {
            try
            {
                if (statusoperation == null)
                    return BadRequest(new { message = "Invalid operation data." });
                _context.StatusOperations.Add(statusoperation);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = statusoperation.Id_EstatusOperacion }, statusoperation);
            }
            catch
            {
                return BadRequest(new { message = "Error creating record" });
            }

        }

        //Update the status of the operations
        [HttpPut("UpdateStatusOperation/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StatusOperation statusoperation)
        {
            try
            {
                if (statusoperation == null || id != statusoperation.Id_EstatusOperacion)
                    return BadRequest(new { message = "\r\nInvalid data" });

                var existingStatusoperation = await _context.StatusOperations.FindAsync(id);
                if (existingStatusoperation == null)
                    return NotFound(new { message = "Record not found" });

                existingStatusoperation.Description = statusoperation.Description;

                _context.StatusOperations.Update(existingStatusoperation);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Registry updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating the record", error = ex.Message });
            }
        }

        [HttpDelete("DeleteStatusOperation/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var statusoperation = await _context.StatusOperations.FindAsync(id);
                if (statusoperation == null)
                    return NotFound(new { message = "Record not found" });

                _context.StatusOperations.Remove(statusoperation);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Record deleted successfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting the record", error = ex.Message });
            }
        }

        [HttpGet("Canceled")]
        [Authorize]
        public async Task<IActionResult> GetOperacionesCanceladas()
        {
            try
            {
                var operaciones = await _context.Database
                    .SqlQuery<OperacionesCanceladasDto>($"EXEC {CallStoreprocedure.GetOperationsCanceled}")
                    .ToListAsync();

                return Ok(operaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error executing stored procedure", error = ex.Message });
            }
        }
    }

}