using CoAlert.Application.Dtos.Comentario;

namespace CoAlert.Application.Interfaces
{
    public interface IComentarioApplicationService
    {
        IEnumerable<ComentarioResponseDto> ObterTodosComentarios();
        ComentarioResponseDto? ObterComentarioPorId(long id);
        ComentarioResponseDto? SalvarComentario(ComentarioRequestDto comentario);
        ComentarioResponseDto? EditarComentario(long id, ComentarioRequestDto comentario);
        ComentarioResponseDto? DeletarComentario(long id);
        IEnumerable<ComentarioResponseDto> ObterComentariosPorPostagem(long postagemId);
        IEnumerable<ComentarioResponseDto> ObterComentariosPorUsuario(long usuarioId);
        IEnumerable<ComentarioResponseDto> ObterRespostasComentario(long idComentarioParente);
        ComentarioResponseDto? IncrementarLike(long id);
    }
} 