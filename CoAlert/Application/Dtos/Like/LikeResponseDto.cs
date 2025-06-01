using System;
using CoAlert.Application.Dtos.Usuario;
using CoAlert.Application.Dtos.Postagem;
using CoAlert.Application.Dtos.Comentario;

namespace CoAlert.Application.Dtos.Like
{
    public class LikeResponseDto
    {
        public long IdLike { get; set; }
        public long UsuarioId { get; set; }
        public long? PostagemId { get; set; }
        public long? ComentarioId { get; set; }
        public DateTime DtLike { get; set; }

        public UsuarioResponseDto Usuario { get; set; }
        public PostagemResponseDto Postagem { get; set; }
        public ComentarioResponseDto Comentario { get; set; }
    }
} 