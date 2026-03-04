using ApiGruposummaOperaciones.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGruposummaOperaciones.Controllers
{
    public class CallOptionDealsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CallOptionDealsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("CallOptionDealsGetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var callOptionsDeals = await _context.CallOptionsDeals.ToListAsync();
                return Ok(callOptionsDeals);
            }
            catch (Exception ex)
            {
                // Puedes loggear el error si tienes un sistema de logging
                return StatusCode(500, $"Ocurrió un error al obtener los datos: {ex.Message}");
            }
        }
    }
}
 