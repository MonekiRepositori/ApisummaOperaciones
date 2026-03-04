using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class SummaByIdOperatuonDto
    {
        [Column("Deal")]
        public string Deal { get; set; } = string.Empty;

        [Column("FechaInicio")]
        public DateTime? FechaInicio { get; set; }

        [Column("NombreCliente")]
        public string NombreCliente { get; set; }

        [Column("Beneficiario")]
        public string Beneficiario { get; set; }

        [Column("MontoUSD")]
        public decimal? MontoUSD { get; set; }

        [Column("TipoCambio")]
        public decimal? TipoCambio { get; set; }

        [Column("TCCliente")]
        public decimal? TCCliente { get; set; }

        [Column("Comision_Porcentaje")]
        public decimal? Comision_Porcentaje { get; set; }

        [Column("Promotor")]
        public string Promotor { get; set; }

        [Column("MontoMXN")]
        public decimal? MontoMXN { get; set; }

        [Column("Comision_Por_Envio_Ahorro")]
        public decimal? Comision_Por_Envio_Ahorro { get; set; }

        [Column("NombrePlataforma")]
        public string PlatformName { get; set; }

        [Column("Mto_CTE_TC")]
        public decimal? Mto_CTE_TC { get; set; }

        [Column("Casque")]
        public decimal? Casque { get; set; }

        [Column("Comision_$")]
        public decimal? Comision_Dolar { get; set; } // Mapea Comision_Dolar a Comision_$ en la base de datos

        [Column("Dep_Cliente")]
        public decimal? Dep_Cliente { get; set; }

        [Column("Utilidad")]
        public decimal? Utilidad { get; set; }

        [Column("FechaCierre")]
        public DateTime? FechaCierre { get; set; } = default(DateTime?);


        [Column("Documento_PDF_Deal")]
        public string Document_Deal_PDF { get; set; } // Mapea Document_Deal_PDF a Documento_PDF_Deal

        [Column("Documento_PDF_FED")]
        public string Documento_PDF_FED { get; set; }

        [Column("Id_Divisas")]
        public int? Id_Divisas { get; set; }

        [Column("Id_EstatusOperacion")]
        public int? Id_EstatusOperacion { get; set; }

        // Agregar estas nuevas propiedades al final
        [Column("DescripcionDivisa")]
        public string? DescripcionDivisa { get; set; }

        [Column("DescripcionEstatus")]
        public string? DescripcionEstatus { get; set; }

        [Column("Id_ticket")]
        public int? Id_Ticket { get; set; }
    }
}
