using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.ModelsDto;
using ApiGruposummaOperaciones.Security;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace ApiGruposummaOperaciones.Controllers
{

    [ApiController]
    [Route("Api/[controller]")]
    public class LoginUserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtServices _jwtServices;

        public LoginUserController(ApplicationDbContext context, JwtServices jwtService)
        {
            _context = context;
            _jwtServices = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto loginRequestDto)
        {
            if (loginRequestDto == null || string.IsNullOrEmpty(loginRequestDto.NombreUsuario) || string.IsNullOrEmpty(loginRequestDto.Contrasena))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            // Buscar el usuario por nombre de usuario
            var usuario = _context.UserRegistration.FirstOrDefault(u => u.Username == loginRequestDto.NombreUsuario);

            if (usuario == null)
            {
                return NotFound(new { message = "The username does not exist." });
            }

            // Validar la contraseña
            if (usuario.Password != loginRequestDto.Contrasena)
            {
                return BadRequest(new { message = "The password is incorrect." });
            }

            // Obtener el rol del usuario
            var userRole = _context.Roles.FirstOrDefault(r => r.Id_Rol == usuario.TipodeUsuario)?.TipoDeRol;

            if (string.IsNullOrEmpty(userRole))
            {
                return BadRequest(new { message = "Role not found for the user." });
            }

            // Generar el JWT token
            var token = _jwtServices.GenerateToken(usuario.UserRecordId, userRole);

            // Generar y almacenar el refresh token
            var refreshToken = _jwtServices.GenerateRefreshTocken();
            refreshToken.RecordId = usuario.UserRecordId;
            _context.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();

            // Retornar la respuesta con el DTO
            return Ok(new LoginResponseDto
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                UserRecordId = usuario.UserRecordId,
                Username = usuario.Username,
                FirstName = usuario.FirstName,
                LastNamePaternal = usuario.LastNamePaternal,
                LastNameMaternal = usuario.LastNameMaternal,
                TipoUsuario = usuario.TipodeUsuario.ToString(),
                TipoDeRol = userRole
            });
        }

        [HttpPost("RefreshToken")]
        public IActionResult Refresh([FromBody] RefreshTokenRequestDto refreshTokenRequest)
        {
            // Buscar el refresh token en la base de datos
            var storedRefreshToken = _context.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshTokenRequest.RefreshToken);

            if (storedRefreshToken == null || storedRefreshToken.DateTimeExpirationDate < DateTime.UtcNow || storedRefreshToken.Revocado)
            {
                return Unauthorized(new { message = "Invalid or expired refresh token." });
            }

            // Buscar el usuario asociado al refresh token
            var usuario = _context.UserRegistration.Find(storedRefreshToken.RecordId);
            if (usuario == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            // Obtener el rol del usuario
            var userRole = _context.Roles.FirstOrDefault(r => r.Id_Rol == usuario.TipodeUsuario)?.TipoDeRol;

            if (string.IsNullOrEmpty(userRole))
            {
                return BadRequest(new { message = "Role not found for the user." });
            }

            // Generar un nuevo JWT token
            var newToken = _jwtServices.GenerateToken(usuario.UserRecordId, userRole);

            // Retornar el nuevo token y el refresh token existente
            return Ok(new RefreshTokenResponseDto
            {
                Token = newToken,
                RefreshToken = storedRefreshToken.Token
            });
        }

    }
}