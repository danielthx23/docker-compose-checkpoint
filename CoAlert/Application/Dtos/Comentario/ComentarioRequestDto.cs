namespace CoAlert.Application.Dtos.Comentario
{
    public class ComentarioRequestDto
    {
        public string NmConteudo { get; set; } = string.Empty;
        public long UsuarioId { get; set; }
        public long PostagemId { get; set; }
        public long? IdComentarioParente { get; set; }
    }
} 