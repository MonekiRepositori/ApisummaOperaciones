using ApiGruposummaOperaciones.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class RegisterUserDto
    {

        [Column("NombreUsuario")]  // Spanish name for the database
        public string Username { get; set; }

        [Column("TipoDeRol")]
        public int TipoDeUsuario { get; set; }
        [Column("Contrasena")]
        public string Password { get; set; }

        [Column("Nombre")]
        public string FirstName { get; set; }

        [Column("ApellidoPaterno")]
        public string LastNamePaternal { get; set; }

        [Column("ApellidoMaterno")]
        public string LastNameMaternal { get; set; }

        [Column("CorreoElectronico")]
        public string Email { get; set; }

        [Column("FechaNacimiento")]
        public DateTime BirthDate { get; set; }

        [Column("Genero")]
        public string Gender { get; set; }

        [Column("Calle")]
        public string Street { get; set; }

        [Column("CodigoPostal")]
        public string PostalCode { get; set; }

        [Column("Estado")]
        public string State { get; set; }

        [Column("Municipio")]
        public string Municipality { get; set; }

        [Column("NoExterior")]
        public string ExteriorNumber { get; set; }

        [Column("NoInterior")]
        public string InteriorNumber { get; set; }



        [Column("Id_Rol")] // Nombre del campo en la tabla Usuario
        public int Id_Rol { get; set; } // Se usa para asociar con la tabla Roles

    }
}
