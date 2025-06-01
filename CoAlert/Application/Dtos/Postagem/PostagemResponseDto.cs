using CoAlert.Application.Dtos.Usuario;
using CoAlert.Application.Dtos.CategoriaDesastre;
using CoAlert.Application.Dtos.Localizacao;
using CoAlert.Application.Dtos.Comentario;

namespace CoAlert.Application.Dtos.Postagem
{
    public class PostagemResponseDto
    {
        public long IdPostagem { get; set; }
        public string NmTitulo { get; set; } = string.Empty;
        public string NmConteudo { get; set; } = string.Empty;
        public DateTime DtEnvio { get; set; }
        public int NrLikes { get; set; }
        public UsuarioResponseDto? Usuario { get; set; }
        public CategoriaDesastreResponseDto? CategoriaDesastre { get; set; }
        public LocalizacaoResponseDto? Localizacao { get; set; }
        public ICollection<ComentarioResponseDto>? Comentarios { get; set; }
    }
} 