using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.Models
{

    [Table("Usuario")]
    public class User
    {
        [Key]
        [Column("Id_Usuario")]
        public int Id_usuario { get; set; } // Clave primaria con IDENTITY

        public int Id_Rol { get; set; } 

        [ForeignKey("Id_Rol")]
        public Role Role { get; set; } // Relación con la tabla Rol (opcional)

    }
}