using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using ApiGruposummaOperaciones.ModelsDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Transactions;
using static ApiGruposummaOperaciones.Operation;

namespace ApiGruposummaOperaciones.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RegisterUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        [Authorize]
        public IActionResult GetAll()
        {
            try
            {
                var users = _context.UserRegistration.ToList();
                if (users.Count == 0)
                {
                    return NotFound(new { message = "No users found." });
                }

                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving users.", error = ex.Message });
            }
        }

        //GetById method to get a user by their ID
        [HttpGet("GetById/{Id}")]
        [Authorize]
        public IActionResult GetbyId(int Id)
        {
            try
            {
                var user = _context.UserRegistration.FirstOrDefault(u => u.UserRecordId == Id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found" });
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the user.", error = ex.Message });
            }
        }

        [HttpPost("RegisterUser")]
        [Authorize]
        public IActionResult Register([FromBody] RegisterUserDto registeruserdto)
        {
            try
            {
                if (registeruserdto == null || string.IsNullOrEmpty(registeruserdto.Username) || string.IsNullOrEmpty(registeruserdto.Password))
                {
                    return BadRequest(new { message = "All fields are required." });
                }

                //Checking if the Register of the user alredy exists
                var existinUser = _context.UserRegistration.FirstOrDefault(u => u.Username == registeruserdto.Username);
                if (existinUser != null)
                {
                    return Conflict(new { message = "The username already exists." });
                }

                //checking if the rol 
                var role = _context.Roles.FirstOrDefault(r => r.Id_Rol == registeruserdto.Id_Rol);
                if (role == null)
                {
                    return BadRequest(new { message = "The specified role does not exist." });
                }


                // Create the new RegisterUser instance
                var newUser = new RegisterUser
                {
                    
                    Username = registeruserdto.Username,
                    TipodeUsuario = registeruserdto.TipoDeUsuario,
                    Password = registeruserdto.Password,
                    FirstName = registeruserdto.FirstName,
                    LastNamePaternal = registeruserdto.LastNamePaternal,
                    LastNameMaternal = registeruserdto.LastNameMaternal,
                    Email = registeruserdto.Email,
                    BirthDate = registeruserdto.BirthDate,
                    Gender = registeruserdto.Gender,
                    Street = registeruserdto.Street,
                    PostalCode = registeruserdto.PostalCode,
                    State = registeruserdto.State,
                    Municipality = registeruserdto.Municipality,
                    ExteriorNumber = registeruserdto.ExteriorNumber,
                    InteriorNumber = registeruserdto.InteriorNumber
                };
                //Add the new user to the context
                _context.UserRegistration.Add(newUser);
                _context.SaveChanges();

                //Obtain the ID of the new user 
                var userId = newUser.UserRecordId;
                // create relation with intermedie table  role

                var userRole = new User
                {
                    Id_usuario = userId,
                    Id_Rol = registeruserdto.Id_Rol
                };
                _context.Users.Add(userRole);
                _context.SaveChanges();

                return Ok(new { message = "User successfully registered." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user.", error = ex.Message });
            }
        }


        [HttpPut("Update/{id}")]
        [Authorize]
        public IActionResult Update(int id, [FromBody] RegisterUserDto registerUserDto)
        {
            try
            {
                var user = _context.UserRegistration.FirstOrDefault(u => u.UserRecordId == id);

                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                // Update fields
                user.Username = registerUserDto.Username;
                user.TipodeUsuario = registerUserDto.TipoDeUsuario;
                user.Password = registerUserDto.Password;
                user.FirstName = registerUserDto.FirstName;
                user.LastNamePaternal = registerUserDto.LastNamePaternal;
                user.LastNamePaternal = registerUserDto.LastNameMaternal;
                user.Email = registerUserDto.Email;
                user.BirthDate = registerUserDto.BirthDate;
                user.Gender = registerUserDto.Gender;
                user.Street = registerUserDto.Street;
                user.PostalCode = registerUserDto.PostalCode;
                user.State = registerUserDto.State;
                user.Municipality = registerUserDto.Municipality;
                user.ExteriorNumber = registerUserDto.ExteriorNumber;
                user.InteriorNumber = registerUserDto.InteriorNumber;

                _context.SaveChanges();

                return Ok(new { message = "User updated succesfully" });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while registering the user.", error = ex.Message });

            }
        }


        [HttpDelete("Delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = await _context.UserRegistration.FindAsync(id);
            if (userId == null)
            {
                return NotFound(new { Message = $"User with {id} no found" });
            }

            try
            {
                _context.UserRegistration.Remove(userId);
                await _context.SaveChangesAsync();
                return Ok(new { mensaje = "Usuario Delete successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = $"Error Deleting User: {ex.Message}" });
            }
        }
    }
}
