using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.Models
{
    [Table("Rol")]
    public class Role
    {
        [Key]
        [Column("Id_Rol")] // Nombre de la columna en la base de datos
        public int Id_Rol { get; set; } // Clave primaria en la tabla de Roles
    
        public string TipoDeRol { get; set; }
    }

}
