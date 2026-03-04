using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class PlatformDto
    {
        [Column("NombrePlataforma")]
        public string PlatformName { get; set; }
    }
}
