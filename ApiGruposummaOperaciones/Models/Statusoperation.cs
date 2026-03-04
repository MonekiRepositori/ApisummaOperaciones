using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.Models
{

    [Table("EstatusOperacion")]
    public class StatusOperation

    {

        [Key]
        [Column("[Id_EstatusOperacion]")]
        public int? Id_EstatusOperacion { get; set; }

        [Column("Descripcion")]
        public string? Description { get; set; }


    }
}
