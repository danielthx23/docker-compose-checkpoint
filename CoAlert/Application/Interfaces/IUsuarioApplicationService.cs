using CoAlert.Application.Dtos.Usuario;
using CoAlert.Domain.Entities;

namespace CoAlert.Application.Interfaces
{   
    public interface IUsuarioApplicationService
    {
        IEnumerable<UsuarioResponseDto> ObterTodosUsuarios();
        UsuarioResponseDto? ObterUsuarioPorId(long id);
        UsuarioResponseDto? SalvarDadosUsuario(UsuarioRequestDto entity);
        UsuarioResponseDto? EditarDadosUsuario(long id, UsuarioRequestDto entity);
        UsuarioResponseDto? DeletarDadosUsuario(long id);
        UsuarioResponseDto? AutenticarUsuario(string emailUsuario, string senha);
    }
}