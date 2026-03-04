using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata;
using static ApiGruposummaOperaciones.Operation;

namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public partial class Operation : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Operation(ApplicationDbContext context)
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
                // Extraer los claims del token JWT
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

                if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userRoleClaim))
                {
                    return Unauthorized("No se pudo validar el usuario.");
                }

                int registroId = int.Parse(userIdClaim); // ID del usuario

                if (!Enum.TryParse(userRoleClaim, true, out TipodeRole tipoRol))

                {
                    return Unauthorized("Rol de usuario no válido.");
                }

                int tipoRolInt = (int)tipoRol;

                // Simulación del SP con LINQ
                var operacionesFiltradasQuery = _context.Operations
                    .Where(o => o.Id_EstatusOperacion == 1 || o.Id_EstatusOperacion == 2 || o.Id_EstatusOperacion == 4);
                if (tipoRolInt > 4)
                {
                    operacionesFiltradasQuery = operacionesFiltradasQuery.Where(o => o.UserRecordId == registroId);
                }

                var operaciones = await operacionesFiltradasQuery
                    .OrderBy(o => o.FechaInicio)
                    .GroupJoin(_context.Currencys, o => o.Id_Divisas, c => c.Id_Divisas,
                     (o, currencyGroup) => new { Operacion = o, CurrencyGroup = currencyGroup })
                    .SelectMany(x => x.CurrencyGroup.DefaultIfEmpty(), (x, currency) => new { x.Operacion, Currency = currency })
                    .GroupJoin(_context.StatusOperations, x => x.Operacion.Id_EstatusOperacion, s => s.Id_EstatusOperacion,
                     (x, statusGroup) => new { x.Operacion, x.Currency, StatusGroup = statusGroup })
                    .SelectMany(x => x.StatusGroup.DefaultIfEmpty(), (x, status) => new OperationSumaDto
                    {
                        Id_Operaciones = x.Operacion.Id_Operaciones,
                        Deal = x.Operacion.Deal,
                        FechaInicio = x.Operacion.FechaInicio,
                        NombreCliente = x.Operacion.NombreCliente,
                        Beneficiario = x.Operacion.Beneficiario,
                        MontoUSD = x.Operacion.MontoUSD,
                        TipoCambio = x.Operacion.TipoCambio,
                        TCCliente = x.Operacion.TCCliente,
                        Comision_Porcentaje = x.Operacion.Comision_Porcentaje,
                        Promotor = x.Operacion.Promotor,
                        MontoMXN = x.Operacion.MontoMXN,
                        Comision_Por_Envio_Ahorro = x.Operacion.Comision_Por_Envio_Ahorro,
                        PlatformName = x.Operacion.Platform.PlatformName,
                        Mto_CTE_TC = x.Operacion.Mto_CTE_TC,
                        Casque = x.Operacion.Casque,
                        Comision_Dolar = x.Operacion.Comision_Dolar,
                        Dep_Cliente = x.Operacion.Dep_Cliente,
                        Utilidad = x.Operacion.Utilidad,
                        FechaCierre = x.Operacion.FechaCierre,
                        DescriptionCurrency = x.Currency != null ? x.Currency.Description : "No hay datos",
                        DescriptionStatus = status != null ? status.Description : "No hay datos",
                        Id_Tickets = x.Operacion.TicketId
                    })

                    .ToListAsync();

                return Ok(operaciones);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting operations: {ex.Message}");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        #region
        //[HttpGet]
        //[Route("GetAll")]
        //[Authorize]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        // Extraer los claims del token JWT
        //        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //        var userRoleClaim = User.FindFirst(ClaimTypes.Role)?.Value;

        //        if (string.IsNullOrEmpty(userIdClaim) || string.IsNullOrEmpty(userRoleClaim))
        //        {
        //            return Unauthorized("No se pudo validar el usuario.");
        //        }

        //        int registroId = int.Parse(userIdClaim); // ID del usuario

        //        // Convertir el rol a la enumeración de manera insensible a mayúsculas/minúsculas
        //        if (!Enum.TryParse(userRoleClaim, true, result: out TipodeRole tipoRol))
        //        {
        //            return Unauthorized("Rol de usuario no válido.");
        //        }

        //        int tipoRolInt = (int)tipoRol;

        //        // Primero llamamos al SP para filtrar las operaciones según el usuario y rol
        //        var operacionesFiltradas = await _context.Operations
        //            .FromSqlInterpolated($"EXEC {CallStoreprocedure.FIlterUser} @RegistroId = {registroId}, @TipoRol = {tipoRolInt}")
        //            //.AsNoTracking()
        //            .ToListAsync();

        //        // Proyección al DTO
        //        var operaciones = await _context.Operations
        //        .Where(o => operacionesFiltradas.Select(of => of.Id_Operaciones).Contains(o.Id_Operaciones))
        //        .OrderBy(o => o.FechaInicio)
        //        .GroupJoin(_context.Currencys, o => o.Id_Divisas, c => c.Id_Divisas,
        //            (o, currencyGroup) => new { Operacion = o, CurrencyGroup = currencyGroup })
        //        .SelectMany(x => x.CurrencyGroup.DefaultIfEmpty(), (x, currency) => new { x.Operacion, Currency = currency })
        //        .GroupJoin(_context.StatusOperations, x => x.Operacion.Id_EstatusOperacion, s => s.Id_EstatusOperacion,
        //            (x, statusGroup) => new { x.Operacion, x.Currency, StatusGroup = statusGroup })
        //        .SelectMany(x => x.StatusGroup.DefaultIfEmpty(), (x, status) => new OperationSumaDto
        //        {
        //           Id_Operaciones = x.Operacion.Id_Operaciones,
        //           Deal = x.Operacion.Deal,
        //           FechaInicio = x.Operacion.FechaInicio,
        //           NombreCliente = x.Operacion.NombreCliente,
        //           Beneficiario = x.Operacion.Beneficiario,
        //           MontoUSD = x.Operacion.MontoUSD,
        //           TipoCambio = x.Operacion.TipoCambio,
        //           TCCliente = x.Operacion.TCCliente,
        //           Comision_Porcentaje = x.Operacion.Comision_Porcentaje,
        //           Promotor = x.Operacion.Promotor,
        //           MontoMXN = x.Operacion.MontoMXN,
        //           Comision_Por_Envio_Ahorro = x.Operacion.Comision_Por_Envio_Ahorro,
        //           PlatformName = x.Operacion.Platform.PlatformName,
        //           Mto_CTE_TC = x.Operacion.Mto_CTE_TC,
        //           Casque = x.Operacion.Casque,
        //           Comision_Dolar = x.Operacion.Comision_Dolar,
        //           Dep_Cliente = x.Operacion.Dep_Cliente,
        //           Utilidad = x.Operacion.Utilidad,
        //           FechaCierre = x.Operacion.FechaCierre,
        //           // Reemplazo de nulls
        //           DescriptionCurrency = x.Currency != null ? x.Currency.Description : "No hay datos",
        //           DescriptionStatus = status != null ? status.Description : "No hay datos",
        //           Id_Tickets = x.Operacion.TicketId

        //        })
        //        .ToListAsync();

        //        return Ok(operaciones);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error (opcional)
        //        Console.WriteLine($"Error getting operations: {ex.Message}");

        //        // Return an error response
        //        return StatusCode(500, "An error occurred while processing the request.");
        //    }
        //}
        #endregion

        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreationOperationDto creationOperationDto)
        {
            if (creationOperationDto == null)
            {
                return BadRequest(new { message = "Invalid operation data." });
            }
            try
            {
                var operation = new OperationSumma()
                {
                    Deal = creationOperationDto.Deal,
                    FechaInicio = creationOperationDto.FechaInicio,
                    NombreCliente = creationOperationDto.NombreCliente,
                    Beneficiario = creationOperationDto.Beneficiario,
                    MontoUSD = creationOperationDto.MontoUSD,
                    TipoCambio = creationOperationDto.TipoCambio,
                    TCCliente = creationOperationDto.TCCliente,
                    Comision_Porcentaje = creationOperationDto.Comision_Porcentaje,
                    Promotor = creationOperationDto.Promotor,
                    MontoMXN = creationOperationDto.MontoMXN,
                    Comision_Por_Envio_Ahorro = creationOperationDto.Comision_Por_Envio_Ahorro,
                    Mto_CTE_TC = creationOperationDto.Mto_CTE_TC,
                    Casque = creationOperationDto.Casque,
                    Comision_Dolar = creationOperationDto.Comision_Dolar,
                    Dep_Cliente = creationOperationDto.Dep_Cliente,
                    Utilidad = creationOperationDto.Utilidad,
                    PlatformId = creationOperationDto.PlatformId,
                    Document_Deal_PDF = creationOperationDto.Document_Deal_PDF,
                    Documento_PDF_FED = creationOperationDto.Documento_PDF_FED,
                    FechaCierre = creationOperationDto.FechaCierre,
                    UserRecordId = creationOperationDto.UserRecordId,
                    Id_Divisas = creationOperationDto.Id_Divisas,
                    Id_EstatusOperacion = creationOperationDto.Id_EstatusOperacion
                };

                // Transacción para garantizar la consistencia
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Operations.Add(operation);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return CreatedAtAction(nameof(GetAll), new { id = operation.Id_Operaciones }, new
                    {
                        message = "Operación creada exitosamente.",
                        operationId = operation.Id_Operaciones
                    });
                }
                catch
                {
                    await transaction.RollbackAsync();
                    throw; // Se lanza la excepción al manejador exterior
                }
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new
                {
                    message = "Error al guardar los datos en la base de datos.",
                    error = dbEx.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocurrió un error inesperado al crear la operación.",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("GetByIdOperaciones/{Id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var operation = await _context.Operations
                  .Include(o => o.Platform)
                  .Include(o => o.Currencys)
                  .Include(o => o.Statusoperations)
                  .Include(o => o.Ticket)
                  .FirstOrDefaultAsync(o => o.Id_Operaciones == Id);
                if (operation == null)
                {
                    return NotFound(new { message = "Operation not found" });
                }


                var summaDto = new SummaByIdOperatuonDto
                {
                    Deal = operation.Deal,
                    FechaInicio = operation.FechaInicio,
                    NombreCliente = operation.NombreCliente,
                    Beneficiario = operation.Beneficiario,
                    MontoUSD = operation.MontoUSD,
                    TipoCambio = operation.TipoCambio,
                    TCCliente = operation.TCCliente,
                    Comision_Porcentaje = operation.Comision_Porcentaje,
                    Promotor = operation.Promotor,
                    MontoMXN = operation.MontoMXN,
                    Comision_Por_Envio_Ahorro = operation.Comision_Por_Envio_Ahorro,
                    PlatformName = operation.Platform.PlatformName,
                    Mto_CTE_TC = operation.Mto_CTE_TC,
                    Casque = operation.Casque,
                    Comision_Dolar = operation.Comision_Dolar,
                    Dep_Cliente = operation.Dep_Cliente,
                    Utilidad = operation.Utilidad,
                    FechaCierre = operation.FechaCierre,
                    Document_Deal_PDF = operation.Document_Deal_PDF,
                    Documento_PDF_FED = operation.Documento_PDF_FED,
                    Id_Divisas = operation.Id_Divisas,
                    Id_EstatusOperacion = operation.Id_EstatusOperacion,
                    DescripcionDivisa = operation.Currencys?.Description ?? "No hay datos",
                    DescripcionEstatus = operation.Statusoperations?.Description ?? "No hay datos",
                    Id_Ticket = operation.Ticket?.Id_Tickets
                };

                return Ok(summaDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving the operation.",

                });
            }
        }

        [HttpPut]
        [Route("Update/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] CreationOperationDto updateOperationDto)
        {
            if (updateOperationDto == null)
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
                //Update  the data only if the DTO  containe  diferent  values 
                operation.Deal = updateOperationDto.Deal ?? operation.Deal;
                operation.FechaInicio = updateOperationDto.FechaInicio ?? operation.FechaInicio;
                operation.NombreCliente = updateOperationDto.NombreCliente ?? operation.NombreCliente;
                operation.Beneficiario = updateOperationDto.Beneficiario ?? operation.Beneficiario;
                operation.MontoUSD = updateOperationDto.MontoUSD;
                operation.TipoCambio = updateOperationDto.TipoCambio;
                operation.TCCliente = updateOperationDto.TCCliente;
                operation.Comision_Porcentaje = updateOperationDto.Comision_Porcentaje;
                operation.Promotor = updateOperationDto.Promotor;
                operation.MontoMXN = updateOperationDto.MontoMXN;
                operation.Comision_Por_Envio_Ahorro = updateOperationDto.Comision_Por_Envio_Ahorro;
                operation.Mto_CTE_TC = updateOperationDto.Mto_CTE_TC;
                operation.Casque = updateOperationDto.Casque;
                operation.Comision_Dolar = updateOperationDto.Comision_Dolar;
                operation.Dep_Cliente = updateOperationDto.Dep_Cliente;
                operation.Utilidad = updateOperationDto.Utilidad;
                operation.PlatformId = updateOperationDto.PlatformId;
                operation.FechaCierre = updateOperationDto.FechaCierre;
                operation.UserRecordId = updateOperationDto.UserRecordId;
                operation.Id_Divisas = updateOperationDto.Id_Divisas;
                operation.Id_EstatusOperacion = updateOperationDto.Id_EstatusOperacion;

                await _context.SaveChangesAsync();

                return Ok(new { message = "Operation updated successfully." });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the operation.", error = ex.Message });
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var operation = await _context.Operations.FindAsync(id);
                if (operation == null)
                {
                    return NotFound(new { message = "Operation not found." });
                }

                //Use a transaction to ensure consistency if there are related operations

                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {

                    _context.Operations.Remove(operation);
                    await _context.SaveChangesAsync();

                    await transaction.CommitAsync();

                    return Ok(new { message = "Operation deleted successfully." });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw; // Relanzamos la excepción para que sea manejada externamente
                }


                _context.Operations.Remove(operation);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Operation deleted successfully." });
            }
            catch (DbUpdateException dbEx)
            {
                return StatusCode(500, new
                {
                    message = "Error deleting the operation from the database.",
                    error = dbEx.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An unexpected error occurred while deleting the operation.", error = ex.Message });
            }
        }
    }
}