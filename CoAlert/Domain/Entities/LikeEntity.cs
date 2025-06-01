using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoAlert.Domain.Entities
{
    [Table("CA_LIKE")]
    public class LikeEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID_LIKE")]
        public long IdLike { get; set; }

        [Required]
        [Column("ID_USUARIO")]
        public long UsuarioId { get; set; }
        public virtual UsuarioEntity Usuario { get; set; }

        [Column("ID_POSTAGEM")]
        public long? PostagemId { get; set; }
        public virtual PostagemEntity? Postagem { get; set; }

        [Column("ID_COMENTARIO")]
        public long? ComentarioId { get; set; }
        public virtual ComentarioEntity? Comentario { get; set; }

        [Required]
        [Column("DT_LIKE")]
        public DateTime DtLike { get; set; }
    }
} 