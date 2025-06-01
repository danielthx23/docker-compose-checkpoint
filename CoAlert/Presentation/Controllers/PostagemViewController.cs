using Microsoft.AspNetCore.Mvc;
using CoAlert.Application.Interfaces;
using CoAlert.Application.Dtos.Postagem;
using CoAlert.Application.Dtos.Comentario;
using CoAlert.Domain.Interfaces;
using CoAlert.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CoAlert.Presentation.Controllers
{
    public class PostagemViewController : Controller
    {
        private readonly IPostagemApplicationService _applicationService;
        private readonly IUsuarioApplicationService _usuarioService;
        private readonly ICategoriaDesastreApplicationService _categoriaService;
        private readonly ILocalizacaoApplicationService _localizacaoService;
        private readonly IComentarioApplicationService _comentarioService;
        private readonly ILikeApplicationService _likeService;

        public PostagemViewController(
            IPostagemApplicationService applicationService,
            IUsuarioApplicationService usuarioService,
            ICategoriaDesastreApplicationService categoriaService,
            ILocalizacaoApplicationService localizacaoService,
            IComentarioApplicationService comentarioService,
            ILikeApplicationService likeService)
        {
            _applicationService = applicationService;
            _usuarioService = usuarioService;
            _categoriaService = categoriaService;
            _localizacaoService = localizacaoService;
            _comentarioService = comentarioService;
            _likeService = likeService;
        }

        public IActionResult Index()
        {
            var postagens = _applicationService.ObterTodasPostagens();
            ViewBag.Usuarios = new SelectList(_usuarioService.ObterTodosUsuarios(), "IdUsuario", "NmUsuario");
            return View(postagens);
        }

        public IActionResult Details(long id)
        {
            var postagem = _applicationService.ObterPostagemPorId(id);
            if (postagem == null)
            {
                return NotFound();
            }

            // Populate users dropdown
            ViewBag.Usuarios = new SelectList(_usuarioService.ObterTodosUsuarios(), "IdUsuario", "NmUsuario");
            
            // Get selected user from query string or use first user as default
            long usuarioId = 0;
            if (long.TryParse(Request.Query["usuarioId"].ToString(), out long selectedUserId))
            {
                usuarioId = selectedUserId;
            }
            else
            {
                var firstUser = _usuarioService.ObterTodosUsuarios().FirstOrDefault();
                if (firstUser != null)
                {
                    usuarioId = firstUser.IdUsuario;
                }
            }

            ViewBag.SelectedUserId = usuarioId;
            
            // Carregar estado dos likes
            ViewBag.PostagemCurtida = _likeService.UsuarioJaCurtiu(usuarioId, id, null);
            
            if (postagem.Comentarios != null)
            {
                var comentariosCurtidos = new Dictionary<long, bool>();
                foreach (var comentario in postagem.Comentarios)
                {
                    comentariosCurtidos[comentario.IdComentario] = 
                        _likeService.UsuarioJaCurtiu(usuarioId, null, comentario.IdComentario);
                }
                ViewBag.ComentariosCurtidos = comentariosCurtidos;
            }

            // Ensure comments are loaded
            if (postagem.Comentarios == null)
            {
                var comentarios = _comentarioService.ObterComentariosPorPostagem(id);
                postagem.Comentarios = comentarios.ToList();
            }

            return View(postagem);
        }

        public IActionResult Create()
        {
            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Categorias = _categoriaService.ObterTodasCategorias();
            ViewBag.Localizacoes = _localizacaoService.ObterTodasLocalizacoes();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PostagemRequestDto postagem)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.SalvarPostagem(postagem);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Categorias = _categoriaService.ObterTodasCategorias();
            ViewBag.Localizacoes = _localizacaoService.ObterTodasLocalizacoes();
            return View(postagem);
        }

        public IActionResult Edit(long id)
        {
            var postagem = _applicationService.ObterPostagemPorId(id);
            if (postagem == null)
            {
                return NotFound();
            }

            var requestDto = new PostagemRequestDto
            {
                NmTitulo = postagem.NmTitulo,
                NmConteudo = postagem.NmConteudo,
                UsuarioId = postagem.Usuario.IdUsuario,
                CategoriaDesastreId = postagem.CategoriaDesastre.IdCategoriaDesastre,
                LocalizacaoId = postagem.Localizacao.IdLocalizacao
            };

            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Categorias = _categoriaService.ObterTodasCategorias();
            ViewBag.Localizacoes = _localizacaoService.ObterTodasLocalizacoes();
            return View(requestDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, PostagemRequestDto postagem)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationService.EditarPostagem(id, postagem);
                if (result != null)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.Usuarios = _usuarioService.ObterTodosUsuarios();
            ViewBag.Categorias = _categoriaService.ObterTodasCategorias();
            ViewBag.Localizacoes = _localizacaoService.ObterTodasLocalizacoes();
            return View(postagem);
        }

        public IActionResult Delete(long id)
        {
            var postagem = _applicationService.ObterPostagemPorId(id);
            if (postagem == null)
            {
                return NotFound();
            }
            return View(postagem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var result = _applicationService.DeletarPostagem(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(long postagemId, ComentarioRequestDto comentario)
        {
            if (ModelState.IsValid)
            {
                comentario.PostagemId = postagemId;
                var result = _comentarioService.SalvarComentario(comentario);
                if (result != null)
                {
                    return RedirectToAction(nameof(Details), new { id = postagemId });
                }
            }
            return RedirectToAction(nameof(Details), new { id = postagemId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LikePost(long id, long usuarioId)
        {
            var postagem = _applicationService.ObterPostagemPorId(id);
            if (postagem == null)
            {
                return NotFound();
            }

            bool jaExisteLike = _likeService.UsuarioJaCurtiu(usuarioId, id, null);
            
            if (jaExisteLike)
            {
                // Remove o like
                if (_likeService.RemoverLike(usuarioId, id, null))
                {
                    int novoTotal = _likeService.ContarLikesPostagem(id);
                    return Json(new { success = true, likes = novoTotal, liked = false });
                }
            }
            else
            {
                // Adiciona novo like
                var resultado = _likeService.SalvarLike(usuarioId, id, null);
                if (resultado != null)
                {
                    int novoTotal = _likeService.ContarLikesPostagem(id);
                    return Json(new { success = true, likes = novoTotal, liked = true });
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LikeComment(long id, long usuarioId)
        {
            var comentario = _comentarioService.ObterComentarioPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }

            bool jaExisteLike = _likeService.UsuarioJaCurtiu(usuarioId, null, id);
            
            if (jaExisteLike)
            {
                // Remove o like
                if (_likeService.RemoverLike(usuarioId, null, id))
                {
                    int novoTotal = _likeService.ContarLikesComentario(id);
                    return Json(new { success = true, likes = novoTotal, liked = false });
                }
            }
            else
            {
                // Adiciona novo like
                var resultado = _likeService.SalvarLike(usuarioId, null, id);
                if (resultado != null)
                {
                    int novoTotal = _likeService.ContarLikesComentario(id);
                    return Json(new { success = true, likes = novoTotal, liked = true });
                }
            }

            return BadRequest();
        }
    }
} 