using CoAlert.Application.Dtos.Comentario;
using CoAlert.Application.Interfaces;
using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;

namespace CoAlert.Application.Services
{
    public class ComentarioApplicationService : IComentarioApplicationService
    {
        private readonly IComentarioRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPostagemRepository _postagemRepository;

        public ComentarioApplicationService(
            IComentarioRepository repository,
            IUsuarioRepository usuarioRepository,
            IPostagemRepository postagemRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _postagemRepository = postagemRepository;
        }

        public IEnumerable<ComentarioResponseDto> ObterTodosComentarios()
        {
            var comentarios = _repository.ObterTodos();
            return comentarios.Select(MapToResponseDto);
        }

        public ComentarioResponseDto? ObterComentarioPorId(long id)
        {
            var comentario = _repository.ObterPorId(id);
            return comentario == null ? null : MapToResponseDto(comentario);
        }

        public ComentarioResponseDto? SalvarComentario(ComentarioRequestDto dto)
        {
            // Validar se as entidades relacionadas existem
            var usuario = _usuarioRepository.ObterPorId((int)dto.UsuarioId);
            var postagem = _postagemRepository.ObterPorId(dto.PostagemId);

            if (usuario == null || postagem == null)
                return null;

            // Se houver um comentário pai, validar se ele existe
            if (dto.IdComentarioParente.HasValue)
            {
                var comentarioPai = _repository.ObterPorId(dto.IdComentarioParente.Value);
                if (comentarioPai == null)
                    return null;
            }

            var entity = new ComentarioEntity
            {
                NmConteudo = dto.NmConteudo,
                DtEnvio = DateTime.UtcNow,
                NrLikes = 0,
                UsuarioId = dto.UsuarioId,
                PostagemId = dto.PostagemId,
                IdComentarioParente = dto.IdComentarioParente
            };

            var resultado = _repository.Salvar(entity);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public ComentarioResponseDto? EditarComentario(long id, ComentarioRequestDto dto)
        {
            var comentarioExistente = _repository.ObterPorId(id);
            if (comentarioExistente == null) return null;

            // Validar se as entidades relacionadas existem
            var usuario = _usuarioRepository.ObterPorId((int)dto.UsuarioId);
            var postagem = _postagemRepository.ObterPorId(dto.PostagemId);

            if (usuario == null || postagem == null)
                return null;

            // Se houver um comentário pai, validar se ele existe
            if (dto.IdComentarioParente.HasValue)
            {
                var comentarioPai = _repository.ObterPorId(dto.IdComentarioParente.Value);
                if (comentarioPai == null)
                    return null;
            }

            comentarioExistente.NmConteudo = dto.NmConteudo;
            comentarioExistente.UsuarioId = dto.UsuarioId;
            comentarioExistente.PostagemId = dto.PostagemId;
            comentarioExistente.IdComentarioParente = dto.IdComentarioParente;

            var resultado = _repository.Atualizar(comentarioExistente);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public ComentarioResponseDto? DeletarComentario(long id)
        {
            var resultado = _repository.Deletar(id);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public IEnumerable<ComentarioResponseDto> ObterComentariosPorPostagem(long postagemId)
        {
            var comentarios = _repository.ObterPorPostagemId(postagemId);
            return comentarios.Select(MapToResponseDto);
        }

        public IEnumerable<ComentarioResponseDto> ObterComentariosPorUsuario(long usuarioId)
        {
            var comentarios = _repository.ObterPorUsuarioId(usuarioId);
            return comentarios.Select(MapToResponseDto);
        }

        public IEnumerable<ComentarioResponseDto> ObterRespostasComentario(long idComentarioParente)
        {
            var comentarios = _repository.ObterRespostas(idComentarioParente);
            return comentarios.Select(MapToResponseDto);
        }

        public ComentarioResponseDto? IncrementarLike(long id)
        {
            var comentario = _repository.ObterPorId(id);
            if (comentario == null) return null;

            comentario.NrLikes++;
            var resultado = _repository.Atualizar(comentario);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        private static ComentarioResponseDto MapToResponseDto(ComentarioEntity comentario)
        {
            return new ComentarioResponseDto
            {
                IdComentario = comentario.IdComentario,
                IdComentarioParente = comentario.IdComentarioParente,
                NmConteudo = comentario.NmConteudo,
                DtEnvio = comentario.DtEnvio,
                NrLikes = comentario.NrLikes,
                Usuario = comentario.Usuario != null ? new Dtos.Usuario.UsuarioResponseDto
                {
                    IdUsuario = comentario.Usuario.IdUsuario,
                    NmUsuario = comentario.Usuario.NmUsuario,
                    NmEmail = comentario.Usuario.NmEmail
                } : null,
                Postagem = comentario.Postagem != null ? new Dtos.Postagem.PostagemResponseDto
                {
                    IdPostagem = comentario.Postagem.IdPostagem,
                    NmTitulo = comentario.Postagem.NmTitulo,
                    NmConteudo = comentario.Postagem.NmConteudo,
                    DtEnvio = comentario.Postagem.DtEnvio,
                    NrLikes = comentario.Postagem.NrLikes
                } : null
            };
        }
    }
} 