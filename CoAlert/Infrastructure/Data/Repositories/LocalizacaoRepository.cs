using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;
using CoAlert.Infrastructure.Data.AppData;

namespace CoAlert.Infrastructure.Data.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository
    {
        private readonly ApplicationContext _context;

        public LocalizacaoRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<LocalizacaoEntity> ObterTodas()
        {
            return _context.Localizacao.ToList();
        }

        public LocalizacaoEntity? ObterPorId(long id)
        {
            return _context.Localizacao.FirstOrDefault(l => l.IdLocalizacao == id);
        }

        public LocalizacaoEntity? Salvar(LocalizacaoEntity localizacao)
        {
            _context.Localizacao.Add(localizacao);
            _context.SaveChanges();
            return localizacao;
        }

        public LocalizacaoEntity? Atualizar(LocalizacaoEntity localizacao)
        {
            _context.Localizacao.Update(localizacao);
            _context.SaveChanges();
            return localizacao;
        }

        public LocalizacaoEntity? Deletar(long id)
        {
            var entity = _context.Localizacao.Find(id);
            if (entity is not null)
            {
                _context.Localizacao.Remove(entity);
                _context.SaveChanges();
                return entity;
            }

            return null;
        }

        public IEnumerable<LocalizacaoEntity> ObterPorCidade(string cidade)
        {
            return _context.Localizacao
                .Where(l => l.NmCidade.ToLower() == cidade.ToLower())
                .ToList();
        }

        public IEnumerable<LocalizacaoEntity> ObterPorEstado(string estado)
        {
            return _context.Localizacao
                .Where(l => l.NmEstado.ToLower() == estado.ToLower())
                .ToList();
        }

        public LocalizacaoEntity? ObterPorCep(string cep)
        {
            return _context.Localizacao
                .FirstOrDefault(l => l.NrCep == cep);
        }
    }
}
