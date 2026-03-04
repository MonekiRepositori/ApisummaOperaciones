using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.Models
{
    [Table("Divisas")]
    public class Foreigncurrencys
    {
        [Key]
        [Column("Id_Divisas")]
        public int? Id_Divisas { get; set; }

        [Column("Descripcion")]
        public string? Description { get; set; }
    }
}
