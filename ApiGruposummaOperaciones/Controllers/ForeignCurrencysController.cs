using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.Data;
using Microsoft.AspNetCore.Mvc.TagHelpers;


namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForeignCurrencysController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ForeignCurrencysController(ApplicationDbContext context)
        {
            _context = context;
        }

        //obtain all the foreign currency
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var currencies = await _context.Currencys.ToListAsync();
                return Ok(currencies);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener las divisas", error = ex.Message });
            }
        }
        // GET A CURRENCY BY ID
        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var currency = await _context.Currencys.FindAsync(id);
                if (currency == null)
                    return NotFound(new { message = "Currency not found" });

                return Ok(currency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error getting currency", error = ex.Message });
            }
        }

        // CREATE A NEW CURRENCY
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Foreigncurrencys currency)
        {
            try
            {
                if (currency == null)
                    return BadRequest(new { message = "Invalid data" });

                _context.Currencys.Add(currency);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = currency.Id_Divisas }, currency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error creating currency", error = ex.Message });
            }
        }

        // UPDATE A CURRENCY
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Foreigncurrencys currency)
        {
            try
            {
                if (currency == null || id != currency.Id_Divisas)
                    return BadRequest(new { message = "Invalid data" });

                var existingCurrency = await _context.Currencys.FindAsync(id);
                if (existingCurrency == null)
                    return NotFound(new { message = "Currency not found" });

                // We just updated the description
                existingCurrency.Description = currency.Description;

                _context.Currencys.Update(existingCurrency);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Currency updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating currency", error = ex.Message });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var currency = await _context.Currencys.FindAsync(id);
                if (currency == null)
                    return NotFound(new { message = "Currency not found" });

                _context.Currencys.Remove(currency);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Currency successfully deleted" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting currency", error = ex.Message });
            }
        }
    }

}
