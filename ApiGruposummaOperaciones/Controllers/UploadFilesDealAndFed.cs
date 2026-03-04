using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UploadFilesDealAndFed : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UploadFilesDealAndFed(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPut]
        [Route("Update/{id}")]
        [Authorize]

        public async Task<IActionResult> UpdatePdfFiles(int id, [FromBody] UploadFilesDeal_and_FedDto updateFilesDto)
        {
            if (updateFilesDto == null)
            {
                return BadRequest(new { message = "Invalid operation data." });
            }

            var operation = await _context.Operations.FindAsync(id);

            if (operation == null)
            {
                return NotFound(new { message = "Operation not found." });
            }

            try
            {
                // Update the PDF fields only if provided in the DTO
                if (!string.IsNullOrEmpty(updateFilesDto.Document_Deal_PDF))
                {
                    operation.Document_Deal_PDF = updateFilesDto.Document_Deal_PDF;
                }

                if (!string.IsNullOrEmpty(updateFilesDto.Documento_PDF_FED))
                {
                    operation.Documento_PDF_FED = updateFilesDto.Documento_PDF_FED;
                }

                await _context.SaveChangesAsync();

                return Ok(new { message = "PDF files updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the PDF files.", error = ex.Message });
            }
        }
    }
}