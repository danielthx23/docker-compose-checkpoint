using CoAlert.Application.Dtos.Localizacao;
using CoAlert.Application.Interfaces;
using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;

namespace CoAlert.Application.Services
{
    public class LocalizacaoApplicationService : ILocalizacaoApplicationService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoApplicationService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<LocalizacaoResponseDto> ObterTodasLocalizacoes()
        {
            var localizacoes = _repository.ObterTodas();
            return localizacoes.Select(MapToResponseDto);
        }

        public LocalizacaoResponseDto? ObterLocalizacaoPorId(long id)
        {
            var localizacao = _repository.ObterPorId(id);
            return localizacao == null ? null : MapToResponseDto(localizacao);
        }

        public LocalizacaoResponseDto? SalvarLocalizacao(LocalizacaoRequestDto dto)
        {
            var entity = new LocalizacaoEntity
            {
                NmBairro = dto.NmBairro,
                NmLogradouro = dto.NmLogradouro,
                NrNumero = dto.NrNumero,
                NmCidade = dto.NmCidade,
                NmEstado = dto.NmEstado,
                NrCep = dto.NrCep,
                NmPais = dto.NmPais,
                DsComplemento = dto.DsComplemento
            };

            var resultado = _repository.Salvar(entity);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public LocalizacaoResponseDto? EditarLocalizacao(long id, LocalizacaoRequestDto dto)
        {
            var localizacaoExistente = _repository.ObterPorId(id);
            if (localizacaoExistente == null) return null;

            localizacaoExistente.NmBairro = dto.NmBairro;
            localizacaoExistente.NmLogradouro = dto.NmLogradouro;
            localizacaoExistente.NrNumero = dto.NrNumero;
            localizacaoExistente.NmCidade = dto.NmCidade;
            localizacaoExistente.NmEstado = dto.NmEstado;
            localizacaoExistente.NrCep = dto.NrCep;
            localizacaoExistente.NmPais = dto.NmPais;
            localizacaoExistente.DsComplemento = dto.DsComplemento;

            var resultado = _repository.Atualizar(localizacaoExistente);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public LocalizacaoResponseDto? DeletarLocalizacao(long id)
        {
            var resultado = _repository.Deletar(id);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public IEnumerable<LocalizacaoResponseDto> ObterLocalizacoesPorCidade(string cidade)
        {
            var localizacoes = _repository.ObterPorCidade(cidade);
            return localizacoes.Select(MapToResponseDto);
        }

        public IEnumerable<LocalizacaoResponseDto> ObterLocalizacoesPorEstado(string estado)
        {
            var localizacoes = _repository.ObterPorEstado(estado);
            return localizacoes.Select(MapToResponseDto);
        }

        public LocalizacaoResponseDto? ObterLocalizacaoPorCep(string cep)
        {
            var localizacao = _repository.ObterPorCep(cep);
            return localizacao == null ? null : MapToResponseDto(localizacao);
        }

        private static LocalizacaoResponseDto MapToResponseDto(LocalizacaoEntity localizacao)
        {
            return new LocalizacaoResponseDto
            {
                IdLocalizacao = localizacao.IdLocalizacao,
                NmBairro = localizacao.NmBairro,
                NmLogradouro = localizacao.NmLogradouro,
                NrNumero = localizacao.NrNumero,
                NmCidade = localizacao.NmCidade,
                NmEstado = localizacao.NmEstado,
                NrCep = localizacao.NrCep,
                NmPais = localizacao.NmPais,
                DsComplemento = localizacao.DsComplemento
            };
        }
    }
} 