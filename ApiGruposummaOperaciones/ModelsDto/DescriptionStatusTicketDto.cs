using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.ModelsDto
{
    [Table("EstatusTicket")]
    public class DescriptionStatusTicketDto
    {

        [Key]
        [Column("[Id_StatusTicket]")]
        public int? Id_StatusTicket { get; set; }

        [Column("Descripcion")]
        public string? Description { get; set; }
    }
}
