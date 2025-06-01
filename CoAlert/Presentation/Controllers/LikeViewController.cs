using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using System.Linq;

namespace CoAlert.Presentation.Controllers
{
    public class LikeViewController : Controller
    {
        private readonly ILikeApplicationService _likeService;
        private readonly IPostagemApplicationService _postagemService;
        private readonly IComentarioApplicationService _comentarioService;
        private readonly IUsuarioApplicationService _usuarioService;

        public LikeViewController(
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

        public IActionResult Index()
        {
            var postagens = _postagemService.ObterTodasPostagens()
                .OrderByDescending(p => p.NrLikes)
                .Take(10)
                .ToList();

            var comentarios = _comentarioService.ObterTodosComentarios()
                .OrderByDescending(c => c.NrLikes)
                .Take(10)
                .ToList();

            ViewBag.TopPostagens = postagens;
            ViewBag.TopComentarios = comentarios;

            return View();
        }

        public IActionResult UserLikes(long userId)
        {
            var usuario = _usuarioService.ObterUsuarioPorId(userId);
            if (usuario == null)
            {
                return NotFound();
            }

            var postagens = _postagemService.ObterTodasPostagens()
                .Where(p => _likeService.UsuarioJaCurtiu(userId, p.IdPostagem, null))
                .ToList();

            var comentarios = _comentarioService.ObterTodosComentarios()
                .Where(c => _likeService.UsuarioJaCurtiu(userId, null, c.IdComentario))
                .ToList();

            ViewBag.Usuario = usuario;
            ViewBag.PostagensCurtidas = postagens;
            ViewBag.ComentariosCurtidos = comentarios;

            return View();
        }

        public IActionResult PostLikes(long postId)
        {
            var postagem = _postagemService.ObterPostagemPorId(postId);
            if (postagem == null)
            {
                return NotFound();
            }

            var usuarios = _usuarioService.ObterTodosUsuarios()
                .Where(u => _likeService.UsuarioJaCurtiu(u.IdUsuario, postId, null))
                .ToList();

            ViewBag.Postagem = postagem;
            ViewBag.UsuariosQueCurtiram = usuarios;

            return View();
        }

        public IActionResult CommentLikes(long commentId)
        {
            var comentario = _comentarioService.ObterComentarioPorId(commentId);
            if (comentario == null)
            {
                return NotFound();
            }

            var usuarios = _usuarioService.ObterTodosUsuarios()
                .Where(u => _likeService.UsuarioJaCurtiu(u.IdUsuario, null, commentId))
                .ToList();

            ViewBag.Comentario = comentario;
            ViewBag.UsuariosQueCurtiram = usuarios;

            return View();
        }
    }
} 