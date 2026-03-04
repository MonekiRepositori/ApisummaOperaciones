namespace ApiGruposummaOperaciones.ModelsDto
{
    public class CreateCommentDto
    {
        public int Id_Ticket { get; set; }
        public string Comentario { get; set; }

        public string? NameUserComment { get; set; }
    }
}
