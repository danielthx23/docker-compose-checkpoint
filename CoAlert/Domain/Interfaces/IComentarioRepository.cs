using CoAlert.Domain.Entities;

namespace CoAlert.Domain.Interfaces
{
    public interface IComentarioRepository
    {
        IEnumerable<ComentarioEntity> ObterTodos();
        ComentarioEntity? ObterPorId(long id);
        ComentarioEntity? Salvar(ComentarioEntity comentario);
        ComentarioEntity? Atualizar(ComentarioEntity comentario);
        ComentarioEntity? Deletar(long id);
        
        IEnumerable<ComentarioEntity> ObterPorPostagemId(long postagemId);
        IEnumerable<ComentarioEntity> ObterPorUsuarioId(long usuarioId);
        IEnumerable<ComentarioEntity> ObterRespostas(long idComentarioParente);
    }
}