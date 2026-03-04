using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.Models
{
    [Table("Plataforma")]
    public class Platform
    {
        [Key]
        [Column("Id_Plataforma")]
        public int Id_BankingPlatform { get; set; }

        [Column("NombrePlataforma")]
        public string PlatformName { get; set; }
      
    }
}
