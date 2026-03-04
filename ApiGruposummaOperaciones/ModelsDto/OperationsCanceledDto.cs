using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ApiGruposummaOperaciones.Models;

namespace ApiGruposummaOperaciones.ModelsDto
{

    public class OperacionesCanceladasDto
    {
        public int Id_Operaciones { get; set; }
        public string Deal { get; set; }
        public DateTime FechaInicio { get; set; }
        public string NombreCliente { get; set; }
        public string Beneficiario { get; set; }
        public decimal MontoUSD { get; set; }
        public decimal TipoCambio { get; set; }        public decimal TCCliente { get; set; }
        public decimal Comision_Porcentaje { get; set; }
        public DateTime? FechaCierre { get; set; }
        public string? Documento_PDF_Deal { get; set; }
        public string? Documento_PDF_FED { get; set; }
        public decimal MontoMXN { get; set; }
        public decimal Comision_Por_Envio_Ahorro { get; set; }
        public decimal Mto_CTE_TC { get; set; }
        public decimal Casque { get; set; }
        [Column("Comision_$")]
        public decimal Comision_Dolar { get; set; }
        public decimal Dep_Cliente { get; set; }
        public decimal Utilidad { get; set; }
        public int RegistroId { get; set; }
        public int Id_Plataforma { get; set; }
        public string Promotor { get; set; }
        public int? Id_Divisas { get; set; }
        public int? Id_EstatusOperacion { get; set; }
        public int? Id_Ticket { get; set; }
        public string? EstatusDescripcion { get; set; }
    }
}



