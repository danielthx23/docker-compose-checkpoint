namespace CoAlert.Application.Dtos.Usuario
{
    public class UsuarioResponseDto
    {
        public long IdUsuario { get; set; }
        public string NmUsuario { get; set; } = string.Empty;
        public string NmEmail { get; set; } = string.Empty;
        public string TokenProvisorio { get; set; } = string.Empty;
    }
} 