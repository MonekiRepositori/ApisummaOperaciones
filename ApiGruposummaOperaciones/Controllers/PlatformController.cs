using Microsoft.AspNetCore.Mvc;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.ModelsDto;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;


namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PlatformController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlatformController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetALLPlataforma")]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var plataform = _context.Platforms.ToList();
                if (plataform == null)
                {
                    return NotFound(new { message = "No plataform found." });
                }

                return Ok(plataform);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving plataforms.", error = ex.Message });
            }
        }

        [HttpPost("CreatePlatform")]
        [Authorize]
        public IActionResult Create([FromBody] PlatformDto platformDto)
        {
            try {
                
                if (platformDto == null || string.IsNullOrEmpty(platformDto.PlatformName))
            {
                return BadRequest(new { message = "Platform name is required" });
            }
            var newPlatform = new Platform
            {
                PlatformName = platformDto.PlatformName
            };
            _context.Platforms.Add(newPlatform);
            _context.SaveChanges();

            return Ok(new { message = "Platform created Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the platform." });
            }
        }

        [HttpPut("UpdatePlataforma/{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] Platform platform)
        {
            try
            {
                var existingPlatform = _context.Platforms.FirstOrDefault(p => p.Id_BankingPlatform == id);

                if (existingPlatform == null)
                {
                    return NotFound(new { message = "Platform not found." });
                }

                if (platform == null || string.IsNullOrEmpty(platform.PlatformName))
                {
                    return BadRequest(new { message = "Platform name is required." });
                }

                // Update platform data

                existingPlatform.PlatformName = platform.PlatformName;
                _context.SaveChanges();

                return Ok(new { message = "Platform updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the platform.", error = ex.Message });
            }
        }

        [HttpDelete("DeletePlataforma/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                var platform = _context.Platforms.FirstOrDefault(p => p.Id_BankingPlatform == id);
                if (platform == null)
                {
                    return NotFound(new { message = "Platform not found." });
                }
                //Delete the platform
                _context.Platforms.Remove(platform);
                _context.SaveChanges();

                return Ok(new { message = "Platform deleted successfully." });
            }
            catch (Exception ex)

            {
                return StatusCode(500, new { message = "An error occurred while deleting the platform.", error = ex.Message });
            }

        }

    }
}

