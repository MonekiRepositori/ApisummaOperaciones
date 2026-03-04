namespace ApiGruposummaOperaciones.ModelsDto
{
    public class AccessMenuRolesDto
    {
        public int Id_MenuRol { get; set; }
        public int Id_Menu { get; set; }
        public int Id_Rol { get; set; }

        public string NombreRol { get; set; }  // opcional
        public string NombreMenu { get; set; } // opcional
    }
}
