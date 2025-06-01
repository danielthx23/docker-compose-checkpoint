namespace CoAlert.Application.Dtos.CategoriaDesastre
{
    public class CategoriaDesastreResponseDto
    {
        public long IdCategoriaDesastre { get; set; }
        public string NmTitulo { get; set; } = string.Empty;
        public string DsCategoria { get; set; } = string.Empty;
        public string NmTipo { get; set; } = string.Empty;
    }
} 