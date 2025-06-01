using CoAlert.Application.Dtos.Localizacao;

namespace CoAlert.Application.Interfaces
{
    public interface ILocalizacaoApplicationService
    {
        IEnumerable<LocalizacaoResponseDto> ObterTodasLocalizacoes();
        LocalizacaoResponseDto? ObterLocalizacaoPorId(long id);
        LocalizacaoResponseDto? SalvarLocalizacao(LocalizacaoRequestDto localizacao);
        LocalizacaoResponseDto? EditarLocalizacao(long id, LocalizacaoRequestDto localizacao);
        LocalizacaoResponseDto? DeletarLocalizacao(long id);
        IEnumerable<LocalizacaoResponseDto> ObterLocalizacoesPorCidade(string cidade);
        IEnumerable<LocalizacaoResponseDto> ObterLocalizacoesPorEstado(string estado);
        LocalizacaoResponseDto? ObterLocalizacaoPorCep(string cep);
    }
} 