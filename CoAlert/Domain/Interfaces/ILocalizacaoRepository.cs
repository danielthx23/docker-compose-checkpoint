using CoAlert.Domain.Entities;

namespace CoAlert.Domain.Interfaces
{
    public interface ILocalizacaoRepository
    {
        IEnumerable<LocalizacaoEntity> ObterTodas();
        LocalizacaoEntity? ObterPorId(long id);
        LocalizacaoEntity? Salvar(LocalizacaoEntity localizacao);
        LocalizacaoEntity? Atualizar(LocalizacaoEntity localizacao);
        LocalizacaoEntity? Deletar(long id);
        
        IEnumerable<LocalizacaoEntity> ObterPorCidade(string cidade);
        IEnumerable<LocalizacaoEntity> ObterPorEstado(string estado);
        LocalizacaoEntity? ObterPorCep(string cep);
    }
}