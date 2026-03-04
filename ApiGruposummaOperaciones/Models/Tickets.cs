
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static ApiGruposummaOperaciones.Status;


namespace ApiGruposummaOperaciones.Models
{
    [Table("Tickets")]
    public class Tickets
    {
        [Key]
        [Column("Id_ticket")]
        public int Id_Tickets { get; set; }

        [Column("Descripcion")]
        public string? Descripcion { get; set; }

        [Column("FechaCreacion")]
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

        [Column("FechaCierre")]
        public DateTime? ClosedDate { get; set; }

        [Column("RegistroId")]
        public int UserRecordId { get; set; }

        [ForeignKey("UserRecordId")]
        public RegisterUser registerUser { get; set; }


        [Column("Id_StatusTicket")]
        public int? TicketStatusId { get; set; }

        [Column("NameUserTicket")]
        public string? NameUserTicket { get; set; }

        [ForeignKey("TicketStatusId")]
        public StatusTicket TicketStatus { get; set; }

        //Relation Foreign key with StatusOperation 
        [Column("Id_EstatusOperacion")]
        public int Id_EstatusOperacion { get; set; }  // Clave foránea
        public StatusOperation Statusoperations { get; set; }

        // Comentarios relacionados
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}

