using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoAlert.Domain.Entities
{
    [Table("CA_POSTAGEM")]
    public class PostagemEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_POSTAGEM")]
        public long IdPostagem { get; set; }

        [Required]
        [Column("NM_TITULO")]
        public string NmTitulo { get; set; }

        [Required]
        [Column("NM_CONTEUDO")]
        public string NmConteudo { get; set; }

        [Required]
        [Column("DT_ENVIO")]
        public DateTime DtEnvio { get; set; }

        [Required]
        [Column("NR_LIKES")]
        public int NrLikes { get; set; }

        [ForeignKey("UsuarioEntity")]
        [Column("CA_USUARIO_ID_USUARIO")]
        public long UsuarioId { get; set; }
        public virtual UsuarioEntity Usuario { get; set; }

        [ForeignKey("CategoriaDesastreEntity")]
        [Column("CA_CATEGORIA_DESASTRE_ID_CATEGORIA_DESASTRE")]
        public long CategoriaDesastreId { get; set; }
        public virtual CategoriaDesastreEntity CategoriaDesastre { get; set; }

        [ForeignKey("LocalizacaoEntity")]
        [Column("CA_LOCALIZACAO_ID_LOCALIZACAO")]
        public long LocalizacaoId { get; set; }
        public virtual LocalizacaoEntity Localizacao { get; set; }

        public virtual ICollection<ComentarioEntity> Comentarios { get; set; } = new List<ComentarioEntity>();
    }
} 