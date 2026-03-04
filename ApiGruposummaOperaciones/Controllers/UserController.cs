using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("ListUsers")]
        
        public IActionResult GetUsuarios()
        {
            try
            {
                // Query using LINQ to obtain the requested information
                var usuarios = _context.UserRegistration
                    .Join(_context.Users,
                        registro => registro.UserRecordId,  // Relates Registro_Usuario with Usuario by RegistroId
                        usuario => usuario.Id_usuario,
                        (registro, usuario) => new { registro, usuario })
                    .Join(_context.Roles,
                        combinado => combinado.registro.TipodeUsuario,  // Relates with Role by the TipodeUsuario field
                        rol => rol.Id_Rol,
                        (combinado, rol) => new
                        {
                            NombreUsuario = combinado.registro.Username,
                            Nombre = combinado.registro.FirstName,
                            ApellidoPaterno = combinado.registro.LastNamePaternal,
                            ApellidoMaterno = combinado.registro.LastNameMaternal,
                            Contrasena = combinado.registro.Password,
                            TipoDeRol = rol.TipoDeRol
                        })
                    .ToList();

                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = ex.Message });
            }
        }
    }
}