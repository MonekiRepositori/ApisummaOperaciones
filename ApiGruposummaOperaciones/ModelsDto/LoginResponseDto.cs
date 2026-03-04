using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public int UserRecordId { get; set; }  // Primary key
        public string RefreshToken { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastNamePaternal { get; set; }
        public string LastNameMaternal { get; set; }
        public string TipoUsuario { get; set; }

        public string TipoDeRol { get; set; }

        //public List<MenuOptionDto> MenuOptions { get; set; }
    }
}
