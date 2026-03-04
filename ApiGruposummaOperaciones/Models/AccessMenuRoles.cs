using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.Models
{
    public class AccessMenuRoles
    {
        public int Id_MenuRol { get; set; }

        [Required]
        public int Id_Menu { get; set; }

        [Required]
        public int Id_Rol { get; set; }

        //Relation
        //public MenuOptions Menu { get; set; }
        public Role Rol { get; set; }

    }
}