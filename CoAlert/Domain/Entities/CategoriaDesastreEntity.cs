using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoAlert.Domain.Entities
{
    [Table("CA_CATEGORIA_DESASTRE")]
    public class CategoriaDesastreEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_CATEGORIA_DESASTRE")]
        public long IdCategoriaDesastre { get; set; }

        [Required]
        [Column("NM_TITULO")]
        public string NmTitulo { get; set; }

        [Required]
        [Column("DS_CATEGORIA")]
        public string DsCategoria { get; set; }

        [Column("NM_TIPO")]
        public string NmTipo { get; set; }
    }
} 