using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.Models
{
    [Table("StatusTicket")]
    public class StatusTicket
    {
        [Key]
        [Column("Id_StatusTicket")]
        public int Id_StatusTicket { get; set; }

        [Column("Descripcion")]
        [Required]
        [MaxLength(50)]
        public string Descripcion { get; set; }

        // Relación con Tickets
        public ICollection<Tickets> Tickets { get; set; } = new List<Tickets>();
    }
}