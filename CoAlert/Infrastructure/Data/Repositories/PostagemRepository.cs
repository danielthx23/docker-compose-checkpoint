using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;
using CoAlert.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CoAlert.Infrastructure.Data.Repositories
{
    public class PostagemRepository : IPostagemRepository
    {
        private readonly ApplicationContext _context;

        public PostagemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<PostagemEntity> ObterTodas()
        {
            return _context.Postagem
                .Include(p => p.Usuario)
                .Include(p => p.CategoriaDesastre)
                .Include(p => p.Localizacao)
                .Include(p => p.Comentarios)
                .ToList();
        }

        public PostagemEntity? ObterPorId(long id)
        {
            return _context.Postagem
                .Include(p => p.Usuario)
                .Include(p => p.CategoriaDesastre)
                .Include(p => p.Localizacao)
                .Include(p => p.Comentarios)
                .FirstOrDefault(p => p.IdPostagem == id);
        }

        public PostagemEntity? Salvar(PostagemEntity entity)
        {
            _context.Postagem.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public PostagemEntity? Atualizar(PostagemEntity entity)
        {
            _context.Postagem.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public PostagemEntity? Deletar(long id)
        {
            var entity = _context.Postagem.Find(id);
            if (entity is not null)
            {
                _context.Postagem.Remove(entity);
                _context.SaveChanges();
                return entity;
            }
            return null;
        }

        public IEnumerable<PostagemEntity> ObterPorUsuarioId(long usuarioId)
        {
            return _context.Postagem
                .Include(p => p.Usuario)
                .Include(p => p.CategoriaDesastre)
                .Include(p => p.Localizacao)
                .Include(p => p.Comentarios)
                .Where(p => p.UsuarioId == usuarioId)
                .ToList();
        }

        public IEnumerable<PostagemEntity> ObterPorCategoriaId(long categoriaDesastreId)
        {
            return _context.Postagem
                .Include(p => p.Usuario)
                .Include(p => p.CategoriaDesastre)
                .Include(p => p.Localizacao)
                .Include(p => p.Comentarios)
                .Where(p => p.CategoriaDesastreId == categoriaDesastreId)
                .ToList();
        }

        public IEnumerable<PostagemEntity> ObterPorLocalizacaoId(long localizacaoId)
        {
            return _context.Postagem
                .Include(p => p.Usuario)
                .Include(p => p.CategoriaDesastre)
                .Include(p => p.Localizacao)
                .Include(p => p.Comentarios)
                .Where(p => p.LocalizacaoId == localizacaoId)
                .ToList();
        }
    }
}
