
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ApiGruposummaOperaciones.Models
{

    [Table("Registro_Usuario")]
    public class RegisterUser
    {
        [Key]
        [Column("RegistroId")]  // Rename to the Spanish name in the database
        public int UserRecordId { get; set; }  // Primary key

        [Column("NombreUsuario")]  // Spanish name for the database
        public string Username { get; set; }

        [Column("TipodeUsuario")]  // Foreign key to Role
        public int TipodeUsuario  { get; set; }  // Foreign key to the Role

        // Foreign key and relationships
        public Role Role { get; set; }  // Relation to the Role table
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

  

        public List<RefreshToken> RefreshTokens { get; set; }

    }
}

