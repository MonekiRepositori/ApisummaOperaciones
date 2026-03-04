using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.Models
{
    public class CallOptionsDeals
    {
        [Key]
        [Column("IdCallOptionsDeals")]
        public int? IdCallOptionsDeals { get; set; }

        [Column("Descripcion")]
        public string? Description { get; set; }
    }
}
