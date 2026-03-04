using ApiGruposummaOperaciones.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class CreationOperationDto

    {
      
        //Campos de entrada necesarios.
        [Column("Deal")]
        public string? Deal { get; set; } = string.Empty;

        [Column("NombreCliente")]
        public string NombreCliente { get; set; }

        [Column("Beneficiario")]
        public string Beneficiario { get; set; }

        [Column("MontoUSD")]
        public decimal MontoUSD { get; set; }


        [Column("TipoCambio")]
        public decimal TipoCambio { get; set; }

        [Column("TCCliente")]
        public decimal TCCliente { get; set; }

        [Column("Comision_Porcentaje")]
        public decimal Comision_Porcentaje { get; set; }

        [Column("Promotor")]
        public string Promotor { get; set; }

        [Column("Id_Plataforma")]
        public int PlatformId { get; set; }


        [Column("Documento_PDF_Deal")]
        public string? Document_Deal_PDF { get; set; }

        [Column("Documento_PDF_FED")]
        public string? Documento_PDF_FED { get; set; }

        [Column("FechaInicio")]
        public DateTime? FechaInicio { get; set; }

        [Column("FechaCierre")]
        public DateTime FechaCierre { get; set; }

        // Resultados calculados

        [Column("MontoMXN")]
        public decimal MontoMXN { get; set; }

        [Column("Comision_Por_Envio_Ahorro")]
        public decimal Comision_Por_Envio_Ahorro { get; set; }

        [Column("Mto_CTE_TC")]
        public decimal Mto_CTE_TC { get; set; }

        [Column("Casque")]
        public decimal Casque { get; set; }

        [Column("Comision_$")]
        public decimal Comision_Dolar { get; set; }

        [Column("Dep_Cliente")]
        public decimal Dep_Cliente { get; set; }

        [Column("Utilidad")]
        public decimal Utilidad { get; set; }

        [ForeignKey("UserRecordId")]
        public int UserRecordId {  get; set; }

        [ForeignKey("Id_Divisas")]
        public int Id_Divisas { get; set; }

        [ForeignKey("Id_EstatusOperacion")]
        public int Id_EstatusOperacion { get; set; }
      
    }
}

