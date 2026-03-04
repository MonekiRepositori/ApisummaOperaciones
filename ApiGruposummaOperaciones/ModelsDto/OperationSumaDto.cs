using ApiGruposummaOperaciones.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class OperationSumaDto
    {
        [Column("Id_Operaciones")]
        public int Id_Operaciones { get; set; }

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

        public string? DescriptionCurrency { get; set; } // New propierty
        public string? DescriptionStatus { get; set; }   // New propierty

        [Column("Id_ticket")]
        public int? Id_Tickets { get; set; }

    }
}

