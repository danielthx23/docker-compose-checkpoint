using CoAlert.Domain.Entities;

namespace CoAlert.Domain.Interfaces
{
    public interface IPostagemRepository
    {
        IEnumerable<PostagemEntity> ObterTodas();
        PostagemEntity? ObterPorId(long id);
        PostagemEntity? Salvar(PostagemEntity postagem);
        PostagemEntity? Atualizar(PostagemEntity postagem);
        PostagemEntity? Deletar(long id);
        
        IEnumerable<PostagemEntity> ObterPorUsuarioId(long usuarioId);
        IEnumerable<PostagemEntity> ObterPorCategoriaId(long categoriaDesastreId);
        IEnumerable<PostagemEntity> ObterPorLocalizacaoId(long localizacaoId);
    }
}