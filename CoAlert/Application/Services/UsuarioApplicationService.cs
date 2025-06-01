using CoAlert.Application.Dtos.Usuario;
using CoAlert.Application.Interfaces;
using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;

namespace CoAlert.Application.Services
{
    public class UsuarioApplicationService : IUsuarioApplicationService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioApplicationService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<UsuarioResponseDto> ObterTodosUsuarios()
        {
            var usuarios = _repository.ObterTodos();
            return usuarios.Select(MapToResponseDto);
        }

        public UsuarioResponseDto? ObterUsuarioPorId(long id)
        {
            var usuario = _repository.ObterPorId(id);
            return usuario == null ? null : MapToResponseDto(usuario);
        }

        public UsuarioResponseDto? SalvarDadosUsuario(UsuarioRequestDto dto)
        {
            var entity = new UsuarioEntity
            {
                NmUsuario = dto.NmUsuario,
                NrSenha = dto.NrSenha,
                NmEmail = dto.NmEmail
            };

            var resultado = _repository.Salvar(entity);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public UsuarioResponseDto? EditarDadosUsuario(long id, UsuarioRequestDto dto)
        {
            var usuarioExistente = _repository.ObterPorId(id);
            if (usuarioExistente == null) return null;

            usuarioExistente.NmUsuario = dto.NmUsuario;
            usuarioExistente.NrSenha = dto.NrSenha;
            usuarioExistente.NmEmail = dto.NmEmail;

            var resultado = _repository.Atualizar(usuarioExistente);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public UsuarioResponseDto? DeletarDadosUsuario(long id)
        {
            var resultado = _repository.Deletar(id);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public UsuarioResponseDto? AutenticarUsuario(string emailUsuario, string senha)
        {
            var resultado = _repository.Autenticar(emailUsuario, senha);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        private static UsuarioResponseDto MapToResponseDto(UsuarioEntity usuario)
        {
            return new UsuarioResponseDto
            {
                IdUsuario = usuario.IdUsuario,
                NmUsuario = usuario.NmUsuario,
                NmEmail = usuario.NmEmail
            };
        }
    }
} 