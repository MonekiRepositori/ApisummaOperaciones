using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class CommentsDto
    {
        public int Id_Comentario { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaComentario { get; set; }
        public int RegistroId { get; set; }
        public int Id_Ticket { get; set; }

        [Column("NameUserComment")]
        public string? NameUserComment { get; set; }
     
        [Column("NombreUsuario")]  // Spanish name for the database
        public string Username { get; set; }


    }
}
