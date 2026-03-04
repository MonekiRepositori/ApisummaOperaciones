using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.Models
{

    [Table("Comentarios")]
    public class Comment
    {
        [Key]
        [Column("Id_Comentario")]
        public int CommentId { get; set; }

        [Column("Id_Ticket")]
        public int TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Tickets Ticket { get; set; }

        [Column("RegistroId")]
        public int UserRecordId { get; set; }

        [Column("NameUserComment")]
        public string? NameUserComment { get; set; }

        [ForeignKey("UserRecordId")]
        public RegisterUser RegisterUser { get; set; }

        [Column("Comentario")]
        public string Comentario { get; set; }

        [Column("FechaComentario")]
        public DateTime CommentDate { get; set; } = DateTime.UtcNow;
    }
}
  