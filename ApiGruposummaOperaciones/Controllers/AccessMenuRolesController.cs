using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class AccessMenuRolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AccessMenuRolesController(ApplicationDbContext context)
        {
            _context = context;
        }
//        [HttpGet]
//        [Route("GetAll")]

//        public async Task<ActionResult<IEnumerable<AccessMenuRoles>>> GetAccessMenuRoles()
//        {
//            try
//            {
//                var menuRoles = await _context.AccessMenuRoles
//                    .Include(mr => mr.Menu)
//                .Include(mr => mr.Rol)
//                    .Select(mr => new AccessMenuRolesDto
//                    {
//                        Id_MenuRol = mr.Id_MenuRol,
//                        Id_Menu = mr.Id_Menu,
//                        Id_Rol = mr.Id_Rol,
//                        NombreMenu = mr.Menu.Nombre,
//                        NombreRol = mr.Rol.TipoDeRol
//                    })
//                    .ToListAsync();

//                return Ok(menuRoles);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Error al obtener los accesos de menú por rol", error = ex.Message });
//            }

//        }

//        [HttpGet]
//        [Route("GetByIdAccesMenuRol/{Id}")]
//        public async Task<ActionResult<AccessMenuRolesDto>> GetAccessMenuRolById(int Id)
//        {
//            try
//            {
//                var accessMenuRol = await _context.AccessMenuRoles
//            .Include(a => a.Menu)
//            .Include(a => a.Rol)
//            .FirstOrDefaultAsync(a => a.Id_MenuRol == Id);

//                if (accessMenuRol == null)
//                    return NotFound(new { message = "Acceso no encontrado" });

//                var dto = new AccessMenuRolesDto
//                {
//                    Id_MenuRol = accessMenuRol.Id_MenuRol,
//                    Id_Menu = accessMenuRol.Id_Menu,
//                    Id_Rol = accessMenuRol.Id_Rol,
//                    NombreMenu = accessMenuRol.Menu?.Nombre,
//                    NombreRol = accessMenuRol.Rol?.TipoDeRol
//                };

//                return Ok(dto);
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Error al obtener acceso", error = ex.Message });
//            }
//        }
//        [HttpPost]
//        [Route("Create")]
//        public async Task<IActionResult> CreateAccess([FromBody] AccessMenuRolesDto accessMenuRolDto)
//        {
//            try
//            {
//                var accessMenuRol = new AccessMenuRoles
//                {
//                    Id_Menu = accessMenuRolDto.Id_Menu,
//                    Id_Rol = accessMenuRolDto.Id_Rol
//                };

//                _context.AccessMenuRoles.Add(accessMenuRol);
//                await _context.SaveChangesAsync();

//                var resultDto = new AccessMenuRolesDto
//                {
//                    Id_MenuRol = accessMenuRol.Id_MenuRol,
//                    Id_Menu = accessMenuRol.Id_Menu,
//                    Id_Rol = accessMenuRol.Id_Rol
//                };

//                return CreatedAtAction(
//                nameof(GetAccessMenuRolById),
//                new { id = accessMenuRol.Id_MenuRol },
//                new
//                {
//                    message = "Acceso asignado correctamente",
//                    data = resultDto
//                });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Error creating access", error = ex.Message });
//            }
//        }
//        [HttpPut]
//        [Route("Update/{Id}")]
//        public async Task<IActionResult> UpdateAccess(int id, [FromBody] AccessMenuRolesDto accessMenuRolDto)
//        {
//            try
//            {
//                var existingAccessMenuRol = await _context.AccessMenuRoles.FindAsync(id);
//                if (existingAccessMenuRol == null)
//                    return NotFound(new { message = "Acceso not Found" });

//                existingAccessMenuRol.Id_Menu = accessMenuRolDto.Id_Menu;
//                existingAccessMenuRol.Id_Rol = accessMenuRolDto.Id_Rol;

//                await _context.SaveChangesAsync();
//                var resultDto = new AccessMenuRolesDto
//                {
//                    Id_MenuRol = existingAccessMenuRol.Id_MenuRol,
//                    Id_Menu = existingAccessMenuRol.Id_Menu,
//                    Id_Rol = existingAccessMenuRol.Id_Rol
//                };

//                return Ok(new { message = "Successfully updated access", data = resultDto });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Error updating access", error = ex.Message });
//            }
//        }
//        [HttpDelete]
//        [Route("Delete/{Id}")]
//        public async Task<IActionResult> DeleteAccess(int id)
//        {
//            try
//            {
//                var accessMenuRol = await _context.AccessMenuRoles.FindAsync(id);
//                if (accessMenuRol == null)
//                    return NotFound(new { message = "Acceso not Found" });

//                _context.AccessMenuRoles.Remove(accessMenuRol);
//                await _context.SaveChangesAsync();

//                return Ok(new { message = "Access deleted successfully" });
//            }
//            catch (Exception ex)
//            {
//                return StatusCode(500, new { message = "Error deleting access", error = ex.Message });
//            }

//        }
   }
}


