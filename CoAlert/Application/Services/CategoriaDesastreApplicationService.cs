using CoAlert.Application.Dtos.CategoriaDesastre;
using CoAlert.Application.Interfaces;
using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;

namespace CoAlert.Application.Services
{
    public class CategoriaDesastreApplicationService : ICategoriaDesastreApplicationService
    {
        private readonly ICategoriaDesastreRepository _repository;

        public CategoriaDesastreApplicationService(ICategoriaDesastreRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CategoriaDesastreResponseDto> ObterTodasCategorias()
        {
            var categorias = _repository.ObterTodas();
            return categorias.Select(MapToResponseDto);
        }

        public CategoriaDesastreResponseDto? ObterCategoriaPorId(long id)
        {
            var categoria = _repository.ObterPorId(id);
            return categoria == null ? null : MapToResponseDto(categoria);
        }

        public CategoriaDesastreResponseDto? SalvarCategoria(CategoriaDesastreRequestDto dto)
        {
            var entity = new CategoriaDesastreEntity
            {
                NmTitulo = dto.NmTitulo,
                DsCategoria = dto.DsCategoria,
                NmTipo = dto.NmTipo
            };

            var resultado = _repository.Salvar(entity);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public CategoriaDesastreResponseDto? EditarCategoria(long id, CategoriaDesastreRequestDto dto)
        {
            var categoriaExistente = _repository.ObterPorId(id);
            if (categoriaExistente == null) return null;

            categoriaExistente.NmTitulo = dto.NmTitulo;
            categoriaExistente.DsCategoria = dto.DsCategoria;
            categoriaExistente.NmTipo = dto.NmTipo;

            var resultado = _repository.Atualizar(categoriaExistente);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public CategoriaDesastreResponseDto? DeletarCategoria(long id)
        {
            var resultado = _repository.Deletar(id);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public IEnumerable<CategoriaDesastreResponseDto> ObterCategoriasPorTipo(string tipo)
        {
            var categorias = _repository.ObterPorTipo(tipo);
            return categorias.Select(MapToResponseDto);
        }

        public IEnumerable<CategoriaDesastreResponseDto> ObterCategoriasPorTitulo(string titulo)
        {
            var categorias = _repository.ObterPorTitulo(titulo);
            return categorias.Select(MapToResponseDto);
        }

        private static CategoriaDesastreResponseDto MapToResponseDto(CategoriaDesastreEntity categoria)
        {
            return new CategoriaDesastreResponseDto
            {
                IdCategoriaDesastre = categoria.IdCategoriaDesastre,
                NmTitulo = categoria.NmTitulo,
                DsCategoria = categoria.DsCategoria,
                NmTipo = categoria.NmTipo
            };
        }
    }
} 