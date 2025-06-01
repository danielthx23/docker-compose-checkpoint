using CoAlert.Application.Dtos.Postagem;

namespace CoAlert.Application.Interfaces
{
    public interface IPostagemApplicationService
    {
        IEnumerable<PostagemResponseDto> ObterTodasPostagens();
        PostagemResponseDto? ObterPostagemPorId(long id);
        PostagemResponseDto? SalvarPostagem(PostagemRequestDto postagem);
        PostagemResponseDto? EditarPostagem(long id, PostagemRequestDto postagem);
        PostagemResponseDto? DeletarPostagem(long id);
        IEnumerable<PostagemResponseDto> ObterPostagensPorUsuario(long usuarioId);
        IEnumerable<PostagemResponseDto> ObterPostagensPorCategoria(long categoriaDesastreId);
        IEnumerable<PostagemResponseDto> ObterPostagensPorLocalizacao(long localizacaoId);
        PostagemResponseDto? IncrementarLike(long id);
    }
} 