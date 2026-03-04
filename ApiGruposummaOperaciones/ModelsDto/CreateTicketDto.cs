using ApiGruposummaOperaciones.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class CreateTicketDto
    {

        [Column("Descripcion")]

        public string? Descripcion { get; set; }
        public DateTime? ClosedDate { get; set; }
        public int? TicketStatusId { get; set; }

        [Column("NameUserTicket")]
        public string? NameUserTicket { get; set; }

        [Column("Id_EstatusOperacion")]
        public int Id_EstatusOperacion { get; set; }  // Clave foránea
                                                      
        public int Id_Operacion { get; set; }  // <-- This is the ID of the operation you want to link the ticket to
    }
}


