using CoAlert.Domain.Entities;

namespace CoAlert.Application.Interfaces
{
    public interface ILikeApplicationService
    {
        bool UsuarioJaCurtiu(long usuarioId, long? postagemId, long? comentarioId);
        LikeEntity? SalvarLike(long usuarioId, long? postagemId, long? comentarioId);
        bool RemoverLike(long usuarioId, long? postagemId, long? comentarioId);
        int ContarLikesPostagem(long postagemId);
        int ContarLikesComentario(long comentarioId);
    }
} 