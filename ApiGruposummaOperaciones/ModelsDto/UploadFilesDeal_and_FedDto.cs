using System.ComponentModel.DataAnnotations.Schema;

namespace ApiGruposummaOperaciones.ModelsDto
{
    public class UploadFilesDeal_and_FedDto
    {

        [Column("Documento_PDF_Deal")]
        public string? Document_Deal_PDF { get; set; } // Mapea Document_Deal_PDF a Documento_PDF_Deal

        [Column("Documento_PDF_FED")]
        public string? Documento_PDF_FED { get; set; }

        [ForeignKey("UserRecordId")]
        public int UserRecordId { get; set; }
    }
}
