namespace CoAlert.Application.Dtos.Localizacao
{
    public class LocalizacaoResponseDto
    {
        public long IdLocalizacao { get; set; }
        public string NmBairro { get; set; } = string.Empty;
        public string NmLogradouro { get; set; } = string.Empty;
        public int NrNumero { get; set; }
        public string NmCidade { get; set; } = string.Empty;
        public string NmEstado { get; set; } = string.Empty;
        public string NrCep { get; set; } = string.Empty;
        public string NmPais { get; set; } = string.Empty;
        public string DsComplemento { get; set; } = string.Empty;
    }
} 