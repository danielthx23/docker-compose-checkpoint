using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoAlert.Domain.Entities
{
    [Table("CA_LOCALIZACAO")]
    public class LocalizacaoEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_LOCALIZACAO")]
        public long IdLocalizacao { get; set; }

        [Required]
        [Column("NM_BAIRRO")]
        public string NmBairro { get; set; }

        [Required]
        [Column("NM_LOGRADOURO")]
        public string NmLogradouro { get; set; }

        [Required]
        [Column("NR_NUMERO")]
        public int NrNumero { get; set; }

        [Required]
        [Column("NM_CIDADE")]
        public string NmCidade { get; set; }

        [Required]
        [Column("NM_ESTADO")]
        public string NmEstado { get; set; }

        [Required]
        [Column("NR_CEP")]
        public string NrCep { get; set; }

        [Required]
        [Column("NM_PAIS")]
        public string NmPais { get; set; }

        [Column("DS_COMPLEMENTO")]
        public string DsComplemento { get; set; }
    }
} 