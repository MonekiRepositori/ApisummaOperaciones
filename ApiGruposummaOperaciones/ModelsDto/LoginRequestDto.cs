using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    /// <summary>
    /// CLASS USE DTO 
    ///  DTO IS 
    /// </summary>
    public class LoginRequestDto
    {
        public string NombreUsuario { get; set; }
        public string Contrasena { get; set; }
    }
}
