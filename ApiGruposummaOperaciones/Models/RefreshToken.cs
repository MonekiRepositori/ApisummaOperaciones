using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.Models
{

        [Table("RefreshToken")]
        public class RefreshToken
        {
            [Key]
            [Column("Id_RefreshTocken")]
            public int Id_RefreshTocken { get; set; }

            [Column("RegistroId")]
            public int RecordId { get; set; }

            [Column("Token ")]
            public string Token { get; set; }

            [Column("FechaCreacion")]
            public DateTime CreationDate { get; set; }

            [Column("FechaExpiracion")]
            public DateTime DateTimeExpirationDate { get; set; }

            [Column("Revocado")]
            public bool Revocado { get; set; }

            // Relación con Registro_Usuario
            public RegisterUser UserRecordId { get; set; }


            public string GetFormattedCreation()
            {
                return CreationDate.ToString("dd-MM-yyyy");

            }
            public string GetFormattedExpiration()
            {
                return DateTimeExpirationDate.ToString("dd-MM-yyyy");

            }

        }
    }

