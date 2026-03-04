using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Name of Chief of Development:Jovani Castro Garcia and Rey Carlos Cano
/// Bussiness: GRUPOSUMMA
/// Proyect:EASYTRASNFER_OPERATIONS
/// Date: 12/102024
/// Description:This Project is for the use of dollar currency operations functions to track, control 
/// Dashboard management in a more updated and technological way.
/// Contact for soport: jcastro@moneki.com and  jovani.castro@gruposumma.com
/// </summary>
namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public RoleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Route("RoleGetAll")]
        [Authorize]
        // Métod but optain  all the rols
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }


        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] RoleCrudDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // We do not assign Role_Id, since it is generated automatically
                var newRol = new Role
                {
                    TipoDeRol = model.TipoDeRol // We assign only the RoleType

                };

                _context.Roles.Add(newRol);
                await _context.SaveChangesAsync();

                // We return the created role, which now includes the generated Role_Id
                return CreatedAtAction(nameof(GetById), new { id = newRol.Id_Rol }, newRol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error creating role: {ex.Message}" });
            }
        }

        // Method to get a role by ID
        [HttpGet("GetById/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var rol = await _context.Roles.FindAsync(id);

            if (rol == null)
            {
                return NotFound(new { Message = $"Role with ID {id} not found." });
            }

            return Ok(rol);
        }

        // Method to update an existing role
        [HttpPut("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] Role model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var rol = await _context.Roles.FindAsync(id) as Role;
            if (rol == null)
            {
                return NotFound(new { Message = $"Role with ID {id} not found." });
            }
            // update the type of role
            rol.TipoDeRol = model.TipoDeRol;

            try
            {
                _context.Roles.Update(rol);
                await _context.SaveChangesAsync();
                return Ok(rol);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error updating role: {ex.Message}" });
            }


        }
        // Method to delete a role
        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound(new { Message = $"Role with ID {id} not found." });
            }

            try
            {
                _context.Roles.Remove(rol);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Role deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error deleting role: {ex.Message}" });
            }
        }
    } 
}