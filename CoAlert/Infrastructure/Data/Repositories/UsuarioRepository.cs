using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;
using CoAlert.Infrastructure.Data.AppData;

namespace CoAlert.Infrastructure.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationContext _context;

        public UsuarioRepository(ApplicationContext context)
        {
            _context = context;
        }

        public IEnumerable<UsuarioEntity> ObterTodos()
        {
            return _context.Usuario.ToList();
        }

        public UsuarioEntity? ObterPorId(long id)
        {
            return _context.Usuario.FirstOrDefault(u => u.IdUsuario == id);
        }

        public UsuarioEntity? Salvar(UsuarioEntity entity)
        {
            _context.Usuario.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public UsuarioEntity? Atualizar(UsuarioEntity entity)
        {
            _context.Usuario.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public UsuarioEntity? Deletar(long id)
        {
            var entity = _context.Usuario.Find(id);
            if (entity != null)
            {
                _context.Usuario.Remove(entity);
                _context.SaveChanges();
                return entity;
            }

            return null;
        }

        public UsuarioEntity? Autenticar(string emailUsuario, string senha)
        {
            return _context.Usuario
                .FirstOrDefault(u =>
                    u.NmEmail.ToLower() == emailUsuario.ToLower() &&
                    u.NrSenha == senha); 
        }
    }
}