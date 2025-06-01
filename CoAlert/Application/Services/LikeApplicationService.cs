using CoAlert.Application.Interfaces;
using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;

namespace CoAlert.Application.Services
{
    public class LikeApplicationService : ILikeApplicationService
    {
        private readonly ILikeRepository _repository;

        public LikeApplicationService(ILikeRepository repository)
        {
            _repository = repository;
        }

        public bool UsuarioJaCurtiu(long usuarioId, long? postagemId, long? comentarioId)
        {
            return _repository.UsuarioJaCurtiu(usuarioId, postagemId, comentarioId);
        }

        public LikeEntity? SalvarLike(long usuarioId, long? postagemId, long? comentarioId)
        {
            var like = new LikeEntity
            {
                UsuarioId = usuarioId,
                PostagemId = postagemId,
                ComentarioId = comentarioId,
                DtLike = DateTime.UtcNow
            };

            return _repository.SalvarLike(like);
        }

        public bool RemoverLike(long usuarioId, long? postagemId, long? comentarioId)
        {
            return _repository.RemoverLike(usuarioId, postagemId, comentarioId);
        }

        public int ContarLikesPostagem(long postagemId)
        {
            return _repository.ContarLikesPostagem(postagemId);
        }

        public int ContarLikesComentario(long comentarioId)
        {
            return _repository.ContarLikesComentario(comentarioId);
        }
    }
} 