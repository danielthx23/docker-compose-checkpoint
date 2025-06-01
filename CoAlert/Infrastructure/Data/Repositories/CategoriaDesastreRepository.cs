using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;
using CoAlert.Infrastructure.Data.AppData;
using Microsoft.EntityFrameworkCore;

namespace CoAlert.Infrastructure.Data.Repositories
{
    public class CategoriaDesastreRepository : ICategoriaDesastreRepository
    {
        private readonly ApplicationContext _context;

        public CategoriaDesastreRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<CategoriaDesastreEntity> ObterTodas()
        {
            return _context.CategoriaDesastre.ToList();
        }

        public CategoriaDesastreEntity? ObterPorId(long id)
        {
            return _context.CategoriaDesastre
                .FirstOrDefault(c => c.IdCategoriaDesastre == id);
        }

        public CategoriaDesastreEntity? Salvar(CategoriaDesastreEntity categoria)
        {
            _context.CategoriaDesastre.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public CategoriaDesastreEntity? Atualizar(CategoriaDesastreEntity categoria)
        {
            _context.CategoriaDesastre.Update(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public CategoriaDesastreEntity? Deletar(long id)
        {
            var entity = _context.CategoriaDesastre.Find(id);
            if (entity is not null)
            {
                _context.CategoriaDesastre.Remove(entity);
                _context.SaveChanges();
                return entity;
            }

            return null;
        }

        public IEnumerable<CategoriaDesastreEntity> ObterPorTipo(string tipo)
        {
            return _context.CategoriaDesastre
                .Where(c => c.NmTipo.ToLower() == tipo.ToLower())
                .ToList();
        }

        public IEnumerable<CategoriaDesastreEntity> ObterPorTitulo(string titulo)
        {
            return _context.CategoriaDesastre
                .Where(c => c.NmTitulo.ToLower().Contains(titulo.ToLower()))
                .ToList();
        }
    }
}
