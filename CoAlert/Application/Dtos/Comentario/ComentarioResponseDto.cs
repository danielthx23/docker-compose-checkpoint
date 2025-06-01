using CoAlert.Application.Dtos.Usuario;
using CoAlert.Application.Dtos.Postagem;

namespace CoAlert.Application.Dtos.Comentario
{
    public class ComentarioResponseDto
    {
        public long IdComentario { get; set; }
        public long? IdComentarioParente { get; set; }
        public string NmConteudo { get; set; } = string.Empty;
        public DateTime DtEnvio { get; set; }
        public long NrLikes { get; set; }
        public UsuarioResponseDto? Usuario { get; set; }
        public PostagemResponseDto? Postagem { get; set; }
    }
} 