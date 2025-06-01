using CoAlert.Domain.Entities;

namespace CoAlert.Domain.Interfaces
{
    public interface ICategoriaDesastreRepository
    {
        IEnumerable<CategoriaDesastreEntity> ObterTodas();
        CategoriaDesastreEntity? ObterPorId(long id);
        CategoriaDesastreEntity? Salvar(CategoriaDesastreEntity categoria);
        CategoriaDesastreEntity? Atualizar(CategoriaDesastreEntity categoria);
        CategoriaDesastreEntity? Deletar(long id);
        
        IEnumerable<CategoriaDesastreEntity> ObterPorTipo(string tipo);
        IEnumerable<CategoriaDesastreEntity> ObterPorTitulo(string titulo);
    }
}