using CoAlert.Domain.Entities;

namespace CoAlert.Domain.Interfaces
{
    public interface ILikeRepository
    {
        bool UsuarioJaCurtiu(long usuarioId, long? postagemId, long? comentarioId);
        LikeEntity? SalvarLike(LikeEntity like);
        bool RemoverLike(long usuarioId, long? postagemId, long? comentarioId);
        int ContarLikesPostagem(long postagemId);
        int ContarLikesComentario(long comentarioId);
    }
} 