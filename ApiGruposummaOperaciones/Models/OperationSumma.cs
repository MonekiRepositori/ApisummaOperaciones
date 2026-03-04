using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ApiGruposummaOperaciones.Models
{
    [Table("Operaciones")]
    public class OperationSumma
    {
        [Key]
        [Column("Id_Operaciones")]
        public int Id_Operaciones { get; set; }

        [Column("RegistroId")]
        public int UserRecordId { get; set; }  // Asegúrate de incluir la clave foránea
                                               // Propiedad de navegación hacia el usuario
        [ForeignKey("UserRecordId")]
        public RegisterUser registerUser { get; set; }

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


        [Column("Mto_CTE_TC")]
        public decimal? Mto_CTE_TC { get; set; }

        [Column("Casque")]
        public decimal? Casque { get; set; }

        [Column("Comision_$")]
        public decimal? Comision_Dolar { get; set; } // Maping Comision_Dolar a Comision_$ in the database

        [Column("Dep_Cliente")]
        public decimal? Dep_Cliente { get; set; }

        [Column("Utilidad")]
        public decimal? Utilidad { get; set; }

        [Column("Documento_PDF_Deal")]
        public string? Document_Deal_PDF { get; set; } // Maping Document_Deal_PDF a Documento_PDF_Deal

        [Column("Documento_PDF_FED")]
        public string? Documento_PDF_FED { get; set; }

        [Column("FechaCierre")]
        public DateTime? FechaCierre { get; set; } = default(DateTime?);

        // Propierties of navigation
        // Relation with Platform
        [Column("Id_Plataforma")]
        public int PlatformId { get; set; }  // Clave foránea
        public Platform Platform { get; set; }


        //Relation foreing key with Foreigncurrencys
        [Column("Id_Divisas")]
        public int Id_Divisas { get; set; }  // Clave foránea 
        public Foreigncurrencys Currencys { get; set; }

        //Relation Foreign key with StatusOperation 
        [Column("Id_EstatusOperacion")]
        public int Id_EstatusOperacion { get; set; }  // Clave foránea
        public StatusOperation Statusoperations { get; set; }

        [Column("Id_Ticket")]
        public int? TicketId { get; set; }

        [ForeignKey("TicketId")]
        public Tickets? Ticket { get; set; }

       
    }
}
