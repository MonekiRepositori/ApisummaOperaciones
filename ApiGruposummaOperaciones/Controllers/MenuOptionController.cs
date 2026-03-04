using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGruposummaOperaciones.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class MenuOptionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MenuOptionController(ApplicationDbContext context)
        {
            _context = context;
        }

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<ActionResult<IEnumerable<MenuOptions>>> GetMenuOptions()
        //{
        //    try
        //    {
        //        var menuOptions = await _context.MenuOptions.ToListAsync();
        //        return Ok(menuOptions);

        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex.Message}");
        //    }
        //}

        ////MenuOption/GetById/
        //[HttpGet]
        //[Route("GetByIdMenuOptions/{id}")]
        //public async Task<ActionResult<MenuOptions>> GetMenuOptionById(int id)
        //{
        //    try
        //    {
        //        var menuOption = await _context.MenuOptions.FindAsync(id);
        //        if (menuOption == null)

        //            return NotFound($"Menu with ID {id} not found");
        //        return Ok(menuOption);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error the searh  the menú: {ex.Message}");
        //    }
        //}

        //[HttpPost]
        //[Route("CreateMenuOptions")]
        //public async Task<ActionResult<MenuOptions>> CreateMenuOption([FromBody] MenuOptionCreateDto dto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    try
        //    {
        //        var newMenu = new MenuOptions
        //        {
        //            Nombre = dto.Nombre,
        //            Rute = dto.Rute,
        //            Icon = dto.Icon
        //        };

        //        _context.MenuOptions.Add(newMenu);
        //        await _context.SaveChangesAsync();

        //        return CreatedAtAction(nameof(GetMenuOptions), new { id = newMenu.Id_MenuOptions }, newMenu);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al crear el menú: {ex.Message}");
        //    }
        //}
        //[HttpPut]
        //[Route("UpdateMenuOptions/{id}")]
        //public async Task<IActionResult> UpdateMenuOption(int id, [FromBody] MenuOptionUpdateDto dto)
        //{
        //    try
        //    {
        //        var menu = await _context.MenuOptions.FindAsync(id);
        //        if (menu == null)
        //            return NotFound($"Menu with ID {id} not found");

        //        menu.Nombre = dto.Nombre;
        //        menu.Rute = dto.Rute;
        //        menu.Icon = dto.Icon;

        //        await _context.SaveChangesAsync();
        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error updating the menu: {ex.Message}");
        //    }
        //}

        //[HttpDelete]
        //[Route("DeleteMenuOptions/{id}")]
        //public async Task<IActionResult> DeleteMenuOption(int id)
        //{
        //    try
        //    {
        //        var menu = await _context.MenuOptions.FindAsync(id);
        //        if (menu == null)
        //            return NotFound();

        //        _context.MenuOptions.Remove(menu);
        //        await _context.SaveChangesAsync();

        //        return NoContent();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error al eliminar el menú: {ex.Message}");
        //    }
        //}
    }
}

