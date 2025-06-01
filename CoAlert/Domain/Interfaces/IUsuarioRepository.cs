using CoAlert.Domain.Entities;

namespace CoAlert.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        IEnumerable<UsuarioEntity> ObterTodos();
        UsuarioEntity? ObterPorId(long id);
        UsuarioEntity? Salvar(UsuarioEntity entity);
        UsuarioEntity? Atualizar(UsuarioEntity entity);
        UsuarioEntity? Deletar(long id);
        UsuarioEntity? Autenticar(string emailUsuario, string senha);
    }
}