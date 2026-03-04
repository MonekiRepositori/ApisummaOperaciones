using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class StatusTicketDto
    {
        [Column("Descripcion")]
        [Required]
        [MaxLength(50)]
        public string Descripcion { get; set; }
    }
}
