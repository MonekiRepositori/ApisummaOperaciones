using ApiGruposummaOperaciones.Models;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class TicketDto
    {
        public int Id_Tickets { get; set; }
        public string? Descripcion { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ClosedDate { get; set; }

        public int? TicketStatusId { get; set; }

        public int UserRecordId { get; set; }

        public string? NameUserTicket { get; set; }

        public string? UserName { get; set; } // Puedes ajustar esto según tu modelo de RegisterUser

        public List<CommentsDto> Comments { get; set; }
        public List<StatusTicketDto> StatusTickets { get; set; } // Cambiado a List<StatusTicketDto>
    }
}
