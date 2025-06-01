using CoAlert.Application.Dtos.Postagem;
using CoAlert.Application.Interfaces;
using CoAlert.Domain.Entities;
using CoAlert.Domain.Interfaces;

namespace CoAlert.Application.Services
{
    public class PostagemApplicationService : IPostagemApplicationService
    {
        private readonly IPostagemRepository _repository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICategoriaDesastreRepository _categoriaRepository;
        private readonly ILocalizacaoRepository _localizacaoRepository;

        public PostagemApplicationService(
            IPostagemRepository repository,
            IUsuarioRepository usuarioRepository,
            ICategoriaDesastreRepository categoriaRepository,
            ILocalizacaoRepository localizacaoRepository)
        {
            _repository = repository;
            _usuarioRepository = usuarioRepository;
            _categoriaRepository = categoriaRepository;
            _localizacaoRepository = localizacaoRepository;
        }

        public IEnumerable<PostagemResponseDto> ObterTodasPostagens()
        {
            var postagens = _repository.ObterTodas();
            return postagens.Select(MapToResponseDto);
        }

        public PostagemResponseDto? ObterPostagemPorId(long id)
        {
            var postagem = _repository.ObterPorId(id);
            return postagem == null ? null : MapToResponseDto(postagem);
        }

        public PostagemResponseDto? SalvarPostagem(PostagemRequestDto dto)
        {
            // Validar se as entidades relacionadas existem
            var usuario = _usuarioRepository.ObterPorId((int)dto.UsuarioId);
            var categoria = _categoriaRepository.ObterPorId(dto.CategoriaDesastreId);
            var localizacao = _localizacaoRepository.ObterPorId(dto.LocalizacaoId);

            if (usuario == null || categoria == null || localizacao == null)
                return null;

            var entity = new PostagemEntity
            {
                NmTitulo = dto.NmTitulo,
                NmConteudo = dto.NmConteudo,
                DtEnvio = DateTime.UtcNow,
                NrLikes = 0,
                UsuarioId = dto.UsuarioId,
                CategoriaDesastreId = dto.CategoriaDesastreId,
                LocalizacaoId = dto.LocalizacaoId
            };

            var resultado = _repository.Salvar(entity);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public PostagemResponseDto? EditarPostagem(long id, PostagemRequestDto dto)
        {
            var postagemExistente = _repository.ObterPorId(id);
            if (postagemExistente == null) return null;

            // Validar se as entidades relacionadas existem
            var usuario = _usuarioRepository.ObterPorId((int)dto.UsuarioId);
            var categoria = _categoriaRepository.ObterPorId(dto.CategoriaDesastreId);
            var localizacao = _localizacaoRepository.ObterPorId(dto.LocalizacaoId);

            if (usuario == null || categoria == null || localizacao == null)
                return null;

            postagemExistente.NmTitulo = dto.NmTitulo;
            postagemExistente.NmConteudo = dto.NmConteudo;
            postagemExistente.UsuarioId = dto.UsuarioId;
            postagemExistente.CategoriaDesastreId = dto.CategoriaDesastreId;
            postagemExistente.LocalizacaoId = dto.LocalizacaoId;

            var resultado = _repository.Atualizar(postagemExistente);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public PostagemResponseDto? DeletarPostagem(long id)
        {
            var resultado = _repository.Deletar(id);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        public IEnumerable<PostagemResponseDto> ObterPostagensPorUsuario(long usuarioId)
        {
            var postagens = _repository.ObterPorUsuarioId(usuarioId);
            return postagens.Select(MapToResponseDto);
        }

        public IEnumerable<PostagemResponseDto> ObterPostagensPorCategoria(long categoriaDesastreId)
        {
            var postagens = _repository.ObterPorCategoriaId(categoriaDesastreId);
            return postagens.Select(MapToResponseDto);
        }

        public IEnumerable<PostagemResponseDto> ObterPostagensPorLocalizacao(long localizacaoId)
        {
            var postagens = _repository.ObterPorLocalizacaoId(localizacaoId);
            return postagens.Select(MapToResponseDto);
        }

        public PostagemResponseDto? IncrementarLike(long id)
        {
            var postagem = _repository.ObterPorId(id);
            if (postagem == null) return null;

            postagem.NrLikes++;
            var resultado = _repository.Atualizar(postagem);
            return resultado == null ? null : MapToResponseDto(resultado);
        }

        private static PostagemResponseDto MapToResponseDto(PostagemEntity postagem)
        {
            return new PostagemResponseDto
            {
                IdPostagem = postagem.IdPostagem,
                NmTitulo = postagem.NmTitulo,
                NmConteudo = postagem.NmConteudo,
                DtEnvio = postagem.DtEnvio,
                NrLikes = postagem.NrLikes,
                Usuario = postagem.Usuario != null ? new Dtos.Usuario.UsuarioResponseDto
                {
                    IdUsuario = postagem.Usuario.IdUsuario,
                    NmUsuario = postagem.Usuario.NmUsuario,
                    NmEmail = postagem.Usuario.NmEmail
                } : null,
                CategoriaDesastre = postagem.CategoriaDesastre != null ? new Dtos.CategoriaDesastre.CategoriaDesastreResponseDto
                {
                    IdCategoriaDesastre = postagem.CategoriaDesastre.IdCategoriaDesastre,
                    NmTitulo = postagem.CategoriaDesastre.NmTitulo,
                    DsCategoria = postagem.CategoriaDesastre.DsCategoria,
                    NmTipo = postagem.CategoriaDesastre.NmTipo
                } : null,
                Localizacao = postagem.Localizacao != null ? new Dtos.Localizacao.LocalizacaoResponseDto
                {
                    IdLocalizacao = postagem.Localizacao.IdLocalizacao,
                    NmBairro = postagem.Localizacao.NmBairro,
                    NmLogradouro = postagem.Localizacao.NmLogradouro,
                    NrNumero = postagem.Localizacao.NrNumero,
                    NmCidade = postagem.Localizacao.NmCidade,
                    NmEstado = postagem.Localizacao.NmEstado,
                    NrCep = postagem.Localizacao.NrCep,
                    NmPais = postagem.Localizacao.NmPais,
                    DsComplemento = postagem.Localizacao.DsComplemento
                } : null,
                Comentarios = postagem.Comentarios?.Select(c => new Dtos.Comentario.ComentarioResponseDto
                {
                    IdComentario = c.IdComentario,
                    NmConteudo = c.NmConteudo,
                    DtEnvio = c.DtEnvio,
                    NrLikes = c.NrLikes,
                    Usuario = c.Usuario != null ? new Dtos.Usuario.UsuarioResponseDto
                    {
                        IdUsuario = c.Usuario.IdUsuario,
                        NmUsuario = c.Usuario.NmUsuario,
                        NmEmail = c.Usuario.NmEmail
                    } : null,
                    IdComentarioParente = c.IdComentarioParente
                }).ToList()
            };
        }
    }
} 