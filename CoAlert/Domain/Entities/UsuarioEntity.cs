using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoAlert.Domain.Entities
{
    [Table("CA_USUARIO")]
    public class UsuarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_USUARIO")]
        public long IdUsuario { get; set; }

        [Required]
        [Column("NM_USUARIO")]
        public string NmUsuario { get; set; }

        [Required]
        [Column("NR_SENHA")]
        public string NrSenha { get; set; }

        [Required]
        [EmailAddress]
        [Column("NM_EMAIL")]
        public string NmEmail { get; set; }
    }
} 