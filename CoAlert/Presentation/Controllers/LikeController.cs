using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Like;
using System.Collections.Generic;
using System.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace CoAlert.Presentation.Controllers
{
    [ApiController]
    [Route("api/like")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeApplicationService _likeService;
        private readonly IPostagemApplicationService _postagemService;
        private readonly IComentarioApplicationService _comentarioService;
        private readonly IUsuarioApplicationService _usuarioService;

        public LikeController(
            ILikeApplicationService likeService,
            IPostagemApplicationService postagemService,
            IComentarioApplicationService comentarioService,
            IUsuarioApplicationService usuarioService)
        {
            _likeService = likeService;
            _postagemService = postagemService;
            _comentarioService = comentarioService;
            _usuarioService = usuarioService;
        }

        [HttpPost("toggle")]
        [SwaggerOperation(
            Summary = "Alternar curtida",
            Description = "Adiciona ou remove uma curtida em uma postagem ou comentário",
            Tags = new[] { "Likes" }
        )]
        [ProducesResponseType(typeof(LikeStatsDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult ToggleLike([FromBody] LikeRequestDto request)
        {
            if (request.PostagemId == null && request.ComentarioId == null)
            {
                return BadRequest("É necessário especificar uma postagem ou um comentário para curtir.");
            }

            if (request.PostagemId != null && request.ComentarioId != null)
            {
                return BadRequest("Não é possível curtir uma postagem e um comentário ao mesmo tempo.");
            }

            var usuario = _usuarioService.ObterUsuarioPorId(request.UsuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            if (request.PostagemId.HasValue)
            {
                var postagem = _postagemService.ObterPostagemPorId(request.PostagemId.Value);
                if (postagem == null)
                {
                    return NotFound("Postagem não encontrada.");
                }
            }

            if (request.ComentarioId.HasValue)
            {
                var comentario = _comentarioService.ObterComentarioPorId(request.ComentarioId.Value);
                if (comentario == null)
                {
                    return NotFound("Comentário não encontrado.");
                }
            }

            bool jaExisteLike = _likeService.UsuarioJaCurtiu(
                request.UsuarioId,
                request.PostagemId,
                request.ComentarioId);

            if (jaExisteLike)
            {
                _likeService.RemoverLike(request.UsuarioId, request.PostagemId, request.ComentarioId);
            }
            else
            {
                _likeService.SalvarLike(request.UsuarioId, request.PostagemId, request.ComentarioId);
            }

            int totalLikes = request.PostagemId.HasValue
                ? _likeService.ContarLikesPostagem(request.PostagemId.Value)
                : _likeService.ContarLikesComentario(request.ComentarioId.Value);

            return Ok(new LikeStatsDto
            {
                TotalLikes = totalLikes,
                IsLiked = !jaExisteLike
            });
        }

        [HttpGet("postagem/{postagemId}")]
        [SwaggerOperation(
            Summary = "Obter curtidas da postagem",
            Description = "Retorna as informações de curtidas de uma postagem específica",
            Tags = new[] { "Likes" }
        )]
        [ProducesResponseType(typeof(LikeStatsDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetPostLikes(long postagemId, [FromQuery] long? usuarioId)
        {
            var postagem = _postagemService.ObterPostagemPorId(postagemId);
            if (postagem == null)
            {
                return NotFound("Postagem não encontrada.");
            }

            var totalLikes = _likeService.ContarLikesPostagem(postagemId);
            var isLiked = usuarioId.HasValue && _likeService.UsuarioJaCurtiu(usuarioId.Value, postagemId, null);

            return Ok(new LikeStatsDto
            {
                TotalLikes = totalLikes,
                IsLiked = isLiked
            });
        }

        [HttpGet("comentario/{comentarioId}")]
        [SwaggerOperation(
            Summary = "Obter curtidas do comentário",
            Description = "Retorna as informações de curtidas de um comentário específico",
            Tags = new[] { "Likes" }
        )]
        [ProducesResponseType(typeof(LikeStatsDto), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetCommentLikes(long comentarioId, [FromQuery] long? usuarioId)
        {
            var comentario = _comentarioService.ObterComentarioPorId(comentarioId);
            if (comentario == null)
            {
                return NotFound("Comentário não encontrado.");
            }

            var totalLikes = _likeService.ContarLikesComentario(comentarioId);
            var isLiked = usuarioId.HasValue && _likeService.UsuarioJaCurtiu(usuarioId.Value, null, comentarioId);

            return Ok(new LikeStatsDto
            {
                TotalLikes = totalLikes,
                IsLiked = isLiked
            });
        }

        [HttpGet("usuario/{usuarioId}")]
        [SwaggerOperation(
            Summary = "Obter curtidas do usuário",
            Description = "Retorna todas as curtidas feitas por um usuário específico",
            Tags = new[] { "Likes" }
        )]
        [ProducesResponseType(typeof(IEnumerable<LikeResponseDto>), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetUserLikes(long usuarioId)
        {
            var usuario = _usuarioService.ObterUsuarioPorId(usuarioId);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var postagens = _postagemService.ObterTodasPostagens()
                .Where(p => _likeService.UsuarioJaCurtiu(usuarioId, p.IdPostagem, null));

            var comentarios = _comentarioService.ObterTodosComentarios()
                .Where(c => _likeService.UsuarioJaCurtiu(usuarioId, null, c.IdComentario));

            var likes = new List<LikeResponseDto>();

            foreach (var postagem in postagens)
            {
                likes.Add(new LikeResponseDto
                {
                    UsuarioId = usuarioId,
                    PostagemId = postagem.IdPostagem,
                    Usuario = usuario,
                    Postagem = postagem
                });
            }

            foreach (var comentario in comentarios)
            {
                likes.Add(new LikeResponseDto
                {
                    UsuarioId = usuarioId,
                    ComentarioId = comentario.IdComentario,
                    Usuario = usuario,
                    Comentario = comentario
                });
            }

            return Ok(likes);
        }
    }
} 