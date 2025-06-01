using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;
using CoAlert.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CoAlert.Infrastructure.Data.Repositories
{
    public class ComentarioRepository : IComentarioRepository
    {
        private readonly ApplicationContext _context;

        public ComentarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<ComentarioEntity> ObterTodos()
        {
            return _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Postagem)
                .ToList();
        }

        public ComentarioEntity? ObterPorId(long id)
        {
            return _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Postagem)
                .FirstOrDefault(c => c.IdComentario == id);
        }

        public ComentarioEntity? Salvar(ComentarioEntity comentario)
        {
            _context.Comentario.Add(comentario);
            _context.SaveChanges();
            return comentario;
        }

        public ComentarioEntity? Atualizar(ComentarioEntity comentario)
        {
            _context.Comentario.Update(comentario);
            _context.SaveChanges();
            return comentario;
        }

        public ComentarioEntity? Deletar(long id)
        {
            var entity = _context.Comentario.Find(id);
            if (entity is not null)
            {
                _context.Comentario.Remove(entity);
                _context.SaveChanges();
                return entity;
            }
            return null;
        }

        public IEnumerable<ComentarioEntity> ObterPorPostagemId(long postagemId)
        {
            return _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Postagem)
                .Where(c => c.PostagemId == postagemId)
                .OrderByDescending(c => c.DtEnvio)
                .ToList();
        }

        public IEnumerable<ComentarioEntity> ObterPorUsuarioId(long usuarioId)
        {
            return _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Postagem)
                .Where(c => c.UsuarioId == usuarioId)
                .ToList();
        }

        public IEnumerable<ComentarioEntity> ObterRespostas(long idComentarioParente)
        {
            return _context.Comentario
                .Include(c => c.Usuario)
                .Include(c => c.Postagem)
                .Where(c => c.IdComentarioParente == idComentarioParente)
                .ToList();
        }
    }
}
