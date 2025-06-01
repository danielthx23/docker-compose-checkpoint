using CoAlert.Application.Dtos.CategoriaDesastre;

namespace CoAlert.Application.Interfaces
{
    public interface ICategoriaDesastreApplicationService
    {
        IEnumerable<CategoriaDesastreResponseDto> ObterTodasCategorias();
        CategoriaDesastreResponseDto? ObterCategoriaPorId(long id);
        CategoriaDesastreResponseDto? SalvarCategoria(CategoriaDesastreRequestDto categoria);
        CategoriaDesastreResponseDto? EditarCategoria(long id, CategoriaDesastreRequestDto categoria);
        CategoriaDesastreResponseDto? DeletarCategoria(long id);
        IEnumerable<CategoriaDesastreResponseDto> ObterCategoriasPorTipo(string tipo);
        IEnumerable<CategoriaDesastreResponseDto> ObterCategoriasPorTitulo(string titulo);
    }
} 