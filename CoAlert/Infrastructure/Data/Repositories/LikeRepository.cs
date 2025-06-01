using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;
using CoAlert.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CoAlert.Infrastructure.Data.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationContext _context;

        public LikeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool UsuarioJaCurtiu(long usuarioId, long? postagemId, long? comentarioId)
        {
            return _context.Like.Count(l => 
                l.UsuarioId == usuarioId && 
                l.PostagemId == postagemId && 
                l.ComentarioId == comentarioId) > 0;
        }

        public LikeEntity? SalvarLike(LikeEntity like)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Like.Add(like);
                _context.SaveChanges();

                // Atualiza o contador de likes na postagem ou comentário
                if (like.PostagemId.HasValue)
                {
                    var postagem = _context.Postagem.Find(like.PostagemId.Value);
                    if (postagem != null)
                    {
                        postagem.NrLikes = _context.Like.Count(l => l.PostagemId == like.PostagemId);
                        _context.Postagem.Update(postagem);
                        _context.SaveChanges();
                    }
                }
                else if (like.ComentarioId.HasValue)
                {
                    var comentario = _context.Comentario.Find(like.ComentarioId.Value);
                    if (comentario != null)
                    {
                        comentario.NrLikes = _context.Like.Count(l => l.ComentarioId == like.ComentarioId);
                        _context.Comentario.Update(comentario);
                        _context.SaveChanges();
                    }
                }

                transaction.Commit();
                return like;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool RemoverLike(long usuarioId, long? postagemId, long? comentarioId)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var like = _context.Like.FirstOrDefault(l => 
                    l.UsuarioId == usuarioId && 
                    l.PostagemId == postagemId && 
                    l.ComentarioId == comentarioId);

                if (like == null)
                    return false;

                _context.Like.Remove(like);
                _context.SaveChanges();

                // Atualiza o contador de likes na postagem ou comentário
                if (postagemId.HasValue)
                {
                    var postagem = _context.Postagem.Find(postagemId.Value);
                    if (postagem != null)
                    {
                        postagem.NrLikes = _context.Like.Count(l => l.PostagemId == postagemId);
                        _context.Postagem.Update(postagem);
                        _context.SaveChanges();
                    }
                }
                else if (comentarioId.HasValue)
                {
                    var comentario = _context.Comentario.Find(comentarioId.Value);
                    if (comentario != null)
                    {
                        comentario.NrLikes = _context.Like.Count(l => l.ComentarioId == comentarioId);
                        _context.Comentario.Update(comentario);
                        _context.SaveChanges();
                    }
                }

                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        public int ContarLikesPostagem(long postagemId)
        {
            return _context.Like.Count(l => l.PostagemId == postagemId);
        }

        public int ContarLikesComentario(long comentarioId)
        {
            return _context.Like.Count(l => l.ComentarioId == comentarioId);
        }
    }
} 