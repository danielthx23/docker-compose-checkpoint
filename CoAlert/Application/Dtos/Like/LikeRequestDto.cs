using System.ComponentModel.DataAnnotations;

namespace CoAlert.Application.Dtos.Like
{
    public class LikeRequestDto
    {
        [Required]
        public long UsuarioId { get; set; }

        public long? PostagemId { get; set; }

        public long? ComentarioId { get; set; }
    }
} 