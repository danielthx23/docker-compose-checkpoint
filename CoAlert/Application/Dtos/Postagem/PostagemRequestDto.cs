namespace CoAlert.Application.Dtos.Postagem
{
    public class PostagemRequestDto
    {
        public string NmTitulo { get; set; } = string.Empty;
        public string NmConteudo { get; set; } = string.Empty;
        public long UsuarioId { get; set; }
        public long CategoriaDesastreId { get; set; }
        public long LocalizacaoId { get; set; }
    }
} 