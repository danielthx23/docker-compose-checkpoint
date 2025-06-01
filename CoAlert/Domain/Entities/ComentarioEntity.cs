using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoAlert.Domain.Entities
{
    [Table("CA_COMENTARIO")]
    public class ComentarioEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_COMENTARIO")]
        public long IdComentario { get; set; }

        [Column("ID_COMENTARIO_PARENTE")]
        public long? IdComentarioParente { get; set; }

        [Required]
        [Column("NM_CONTEUDO")]
        public string NmConteudo { get; set; }

        [Required]
        [Column("DT_ENVIO")]
        public DateTime DtEnvio { get; set; }

        [Required]
        [Column("NR_LIKES")]
        public long NrLikes { get; set; }

        [ForeignKey("UsuarioEntity")]
        [Column("ID_USUARIO")]
        public long UsuarioId { get; set; }
        public virtual UsuarioEntity Usuario { get; set; }

        [ForeignKey("PostagemEntity")]
        [Column("CA_POSTAGEM_ID_POSTAGEM")]
        public long PostagemId { get; set; }
        public virtual PostagemEntity Postagem { get; set; }
    }
} 